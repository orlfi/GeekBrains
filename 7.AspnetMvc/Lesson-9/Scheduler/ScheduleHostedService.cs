using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace Scheduler
{
    public class ScheduleHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        
        private readonly JobSchedule _jobSchedule;
        private readonly IJobFactory _jobFactory;

        public IScheduler Scheduler { get; set; }
        
        public ScheduleHostedService(ISchedulerFactory schedulerFactory, JobSchedule jobSchedule, IJobFactory jobFactory)
        {
            _schedulerFactory= schedulerFactory;
            _jobSchedule = jobSchedule;
            _jobFactory= jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken).ConfigureAwait(false);
            Scheduler.JobFactory = _jobFactory;

            IJobDetail job = CreateJobDetail(_jobSchedule);

            var trigger = CreateTrigger(_jobSchedule);

            await Scheduler.ScheduleJob(job, trigger).ConfigureAwait(false);

            await Scheduler.Start(cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken)!;
        }

        private static IJobDetail CreateJobDetail(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}
