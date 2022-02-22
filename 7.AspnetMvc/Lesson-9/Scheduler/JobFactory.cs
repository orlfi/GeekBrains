using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using Scheduler.Jobs;

namespace Scheduler;

public sealed class JobFactory : IJobFactory
{
    private readonly IServiceProvider _services;

    public JobFactory(IServiceProvider services)
    {
        _services = services;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var job = _services.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        return job;
    }

    public void ReturnJob(IJob job)
    {
    }
}
