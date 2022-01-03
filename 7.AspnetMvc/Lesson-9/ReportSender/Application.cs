using Microsoft.Extensions.Logging;
using ReportSender.Interfaces;

namespace MailService;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly IReportManager _reportManager;

    private readonly ILogger _logger;


    public Application(IReportManager reportManager, ILogger<Application> logger)
    {
        _reportManager = reportManager;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancel = default)
    {
        try
        {
            _logger.LogInformation("Application started");
            _reportManager.PrintEmployeesReports();
            await _reportManager.SendEmployeesReportsAsync(cancel);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }

}