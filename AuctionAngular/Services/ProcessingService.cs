using AuctionAngular.Interfaces;
using Quartz.Impl;
using Quartz.Logging;
using Quartz;
using Database;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public sealed class ProcessingService : IProcessingService
    {

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<HelloJob>().Build();


            DateTime dateTime = new DateTime(2023, 7, 29, 16, 53, 0);


            for (int i = 0; i < 8; i++)
            {
                DateTimeOffset triggerTime = new DateTimeOffset(dateTime, TimeSpan.FromHours(2));

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                     .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(1)
                      .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(job, trigger);

                Console.WriteLine("Press any key to stop");
                Console.ReadKey();

                await scheduler.Shutdown();
            }
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
    }

    public class HelloJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }


}