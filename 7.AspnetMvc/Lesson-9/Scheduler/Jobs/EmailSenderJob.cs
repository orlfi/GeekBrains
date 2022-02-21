using Microsoft.Extensions.Logging;
using Quartz;
using ReportSender.Interfaces;
using ReportSender.Services;

namespace Scheduler.Jobs;

public class EmailSenderJob : IJob
{
    private readonly ILogger<EmailSenderJob> _logger;
    private readonly IReportManager _reportManager;

    public EmailSenderJob(IReportManager reportManager, ILogger<EmailSenderJob> logger)
    {
        _logger = logger;
        _reportManager = reportManager;
    }
    public async  Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Выполнение задания по расписанию...");
        _reportManager.PrintEmployeesReports();
        await _reportManager.SendEmployeesReportsAsync(context.CancellationToken).ConfigureAwait(false);
        _logger.LogInformation("Задание по расписанию выполнено");
    }
}
