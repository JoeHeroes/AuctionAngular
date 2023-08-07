using AuctionAngular.Interfaces;
using Quartz;

namespace AuctionAngular.Services
{
    public class ProcessingJob : IJob
    {

        private readonly IServiceProvider _provider;
        public ProcessingJob(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IAuctionService>();
                Console.Out.WriteLineAsync("Greetings from HelloJob!");


                var list = service.AuctionListSpecial();


                foreach (var item in list)
                {
                    Console.Out.WriteLineAsync(item.Fuel.ToString());
                }
            }

            return Task.CompletedTask;
        }
    }
}