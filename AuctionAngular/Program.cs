using AuctionAngular.Services;
using AuctionAngular;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using FluentValidation.AspNetCore;
using CarAuction.Seeder;
using FluentValidation;
using NLog;
using Microsoft.EntityFrameworkCore;
using AuctionAngular.Interfaces;
using Database.Entities;
using Database.Entities.Validators;
using Database;
using System.Reflection;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using AuctionAngular.Background;
using AuctionAngular.Dtos.User;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder();

    //Nlog
    builder.Services.AddControllersWithViews();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //services.AddTransient<>();           
    var authenticationSettings = new AuthenticationSettings();

    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

    //Autoorization
    builder.Services.AddSingleton(authenticationSettings);

    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
        };
    });

    //Validator
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    //Sedder
    builder.Services.AddScoped<AuctionSeeder>();

    //Scheduler

    builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
    builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

    // Add our job
    builder.Services.AddSingleton<ProcessingJob>();
    builder.Services.AddSingleton(new JobSchedule(
        jobType: typeof(ProcessingJob),
        cronExpression: "0/1 * * * * ?")); // run every 5 seconds

    //Hosted
    builder.Services.AddHostedService<QuartzHostedService>();

    //Interface
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IAuctionService, AuctionService>();
    builder.Services.AddScoped<ICalendarService, CalendarService>();
    builder.Services.AddScoped<IInvoiceService, InvoiceService>();
    builder.Services.AddScoped<ILocationService, LocationService>();
    builder.Services.AddTransient<IMailService, MailService>();
    builder.Services.AddTransient<IMessageService, MessageService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<IVehicleService, VehicleService>();

    //Hasser
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    //Validetor
    builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

    //ContextAccessor
    builder.Services.AddHttpContextAccessor();

    //Swagger
    builder.Services.AddSwaggerGen(x =>
    {
        var xmlFail = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFail);
        x.IncludeXmlComments(xmlPath);
    });

    //Cors
    builder.Services.AddCors(
        options => options.AddPolicy("FrontEndClient", policyBuilder =>
        policyBuilder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(builder.Configuration["AllowedOrigins"])
        ));


    //Mail

    builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

    //DbContext
    builder.Services.AddDbContext<AuctionDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    var app = builder.Build();

    var scope = app.Services.CreateScope();

    var seeder = scope.ServiceProvider.GetRequiredService<AuctionSeeder>();

    app.UseResponseCaching();

    app.UseStaticFiles();

    app.UseCors("FrontEndClient");

    seeder.Seed();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseSwagger();

    app.UseSwaggerUI(x => 
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0")    
    );


    

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception exception)
{

    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}