using AuctionAngular.Interfaces;

namespace AuctionAngular.Services
{
    public sealed class HostBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public HostBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IProcessingService scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<IProcessingService>();

                await scopedProcessingService.DoWorkAsync(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }
    }
}