using MailService.DAL;
using MailService.DAL.Repositories;
using MailService.Data;
using MailService.Domain.Entities;
using MailService.Interfaces.Services;
using Microsoft.Extensions.Logging;
using ReportSender.Interfaces.Reports;
using System.Diagnostics;

namespace MailService;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly ILogger _logger;
    private readonly IMemoryRepository<Employee> _employees;
    private readonly IEmployeeReport _report;
    private readonly IMailGatewayBuilder _mailGatewayBuilder;
    private IGateway _mailGateway;

    public Application(ILogger<Application> logger, IMemoryRepository<Employee> employees, IEmployeeReport report, IMailGatewayBuilder mailGatewayBuilder)
    {
        _logger = logger;
        _employees = employees;
        _report = report;
        _mailGatewayBuilder = mailGatewayBuilder;
    }

    public async Task RunAsync(CancellationToken cancel = default)
    {
        try
        {
            _mailGateway = await _mailGatewayBuilder.BuildAsync(cancel).ConfigureAwait(false);
            //_mailGateway = _mailGatewayBuilder.Build();
            await Task.Run(() => _logger.LogInformation("Application started"));
            foreach (var item in _employees.GetAll())
            {
                Console.WriteLine("{0} {1}", item.Name, item.Email);
                foreach (var order in item.Orders)
                {
                    Console.WriteLine("\t{0,-20} {1,10} {2,10} {3,10}", order.Product.Name, order.Product.Price, order.Count, order.Total);
                }
            }

            var employee = _employees.Get(1);
            if (employee is null)
                return;

            await SendEmployeeReportByMail(employee, cancel).ConfigureAwait(false);

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }

    private async Task SendEmployeeReportByMail(Employee employee, CancellationToken cancel = default)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        string report = await _report.CreateAsync(employee, cancel).ConfigureAwait(false);

        var message = new Message
        {
            Body = report,
            Subject = "Заказы сотрудника",
            IsHtml = true,
            Name = "Сервис",
            To = "orlfi@mail.ru"
        };

        await _mailGateway.SendAsync(message, cancel);
    }
}