using MailService.DAL;
using MailService.DAL.Repositories;
using MailService.Data;
using MailService.Domain.Entities;
using MailService.Interfaces.Services;
using Microsoft.Extensions.Logging;
using ReportSender.Interfaces.Reports;
using ReportSender.Services;
using System.Diagnostics;

namespace MailService;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly ReportManager _reportManager;

    private readonly ILogger _logger;


    public Application(ReportManager reportManager, ILogger<Application> logger)
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