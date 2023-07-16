
using AuctionAngular.Interfaces;

namespace AuctionAngular.Services
{
    public class SchedulerService : IHostedService, IDisposable
    {
        private int executionCount = 0;

        private Timer _timerNotification;
        public IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public SchedulerService(IServiceScopeFactory serviceScopeFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timerNotification = new Timer(callback: RunJob, null, TimeSpan.Zero, TimeSpan.FromMicroseconds(1));
            return Task.CompletedTask;
        }

        private void RunJob(object state)
        {
            using (var scrope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    Console.WriteLine("Lol");
                    //var store = scrope.ServiceProvider.GetService<IAuctionService>();

                }
                catch (Exception ex) { }

                Interlocked.Increment(ref executionCount);
            }
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
