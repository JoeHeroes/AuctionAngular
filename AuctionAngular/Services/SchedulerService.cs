using Quartz.Impl;
using Quartz.Logging;
using Quartz;
using Database;

namespace AuctionAngular.Services
{
    public class SchedulerService : IHostedService, IDisposable
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SchedulerService(AuctionDbContext dbContext, IServiceScopeFactory serviceScopeFactory)
        {
            _dbContext = dbContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            DateTime dateTime = new DateTime(2023, 7, 20, 14, 44, 0);
            DateTimeOffset triggerTime = new DateTimeOffset(dateTime, TimeSpan.FromHours(2));



            //List<Auction> auctions = new List<Auction>();
            //List<DateTimeOffset> triggerTimes = new List<DateTimeOffset>();

            //foreach (var auction in auctions)
            //{
            //    triggerTimes.Add(new DateTimeOffset(auction.date, TimeSpan.FromHours(2)));
            //}

            //foreach (var trig in triggerTimes)
            //{
            //    ITrigger trigger = TriggerBuilder.Create()
            //     .WithIdentity("trigger1", "group1")
            //     .StartAt(trig)
            //     .Build();

            //    await scheduler.ScheduleJob(job, trigger);
            //}


            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(triggerTime)
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            Console.WriteLine("Press any key to stop");
            Console.ReadKey();

            await scheduler.Shutdown();
        }


        private class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= Quartz.Logging.LogLevel.Info && func != null)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                    }
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
            {
                throw new NotImplementedException();
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

    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
