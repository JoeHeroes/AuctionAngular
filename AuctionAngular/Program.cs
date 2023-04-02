using AuctionAngular.DTO;
using AuctionAngular.Models;
using AuctionAngular.Services;
using AuctionAngular;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using NLog.Web;
using FluentValidation.AspNetCore;
using AuctionAngular.Authorization.Policy;
using CarAuction.Seeder;
using AuctionAngular.Middleware;
using FluentValidation;
using NLog;
using Microsoft.EntityFrameworkCore;
using AuctionAngular.Models.Validators;
using AuctionAngular.Services.Interface;

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

    builder.Services.AddAuthorization(options => {
        options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "German", "Polish"));
        options.AddPolicy("Atleast20", builder => builder.AddRequirements(new MinimumAgeRequirment(20)));
    });

    //RequirmentHandler
    builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirmentHandler>();

    //Validator
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    //Sedder
    builder.Services.AddScoped<AuctionSeeder>();

    //Interface
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IVehicleService, VehicleService>();
    builder.Services.AddScoped<ILocationService, LocationService>();


    //Middleware
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<RequestTimeMiddleware>();

    //Hasser
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    //Validetor
    builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

    //ContextAccessor
    builder.Services.AddHttpContextAccessor();

    //Swagger
    builder.Services.AddSwaggerGen();

    //Cors
    builder.Services.AddCors(
        options => options.AddPolicy("FrontEndClient", policyBuilder =>
        policyBuilder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(builder.Configuration["AllowedOrigins"])
        ));

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

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeMiddleware>();

    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseSwagger();

    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0"));

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