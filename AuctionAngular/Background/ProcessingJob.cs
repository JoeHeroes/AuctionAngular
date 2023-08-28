using AuctionAngular.Interfaces;
using Quartz;

namespace AuctionAngular.Background
{
    public class ProcessingJob : IJob
    {
        private readonly IServiceProvider _provider;
        public ProcessingJob(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IAuctionService>();
                Console.Out.WriteLineAsync("...");

                bool live = await service.LiveAuctionAsync();

                bool start = await service.StartedAuctionAsync();

                if (live && !start)
                {
                    Console.Out.WriteLineAsync("Auction Start!!!");
                    await service.StartAuctionAsync();
                }
            }
        }
    }
}