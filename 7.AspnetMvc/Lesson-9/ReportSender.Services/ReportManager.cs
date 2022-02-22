using Microsoft.Extensions.Logging;
using ReportSender.Domain;
using ReportSender.Domain.Entities;
using ReportSender.Interfaces;
using ReportSender.Interfaces.Gateways;
using ReportSender.Interfaces.Reports;
using ReportSender.Interfaces.Repositories;

namespace ReportSender.Services;

public sealed class ReportManager : IReportManager
{
    private readonly ILogger<IReportManager> _logger;

    private readonly IMemoryRepository<Employee> _employees;

    private readonly IEmployeeReport _report;

    private readonly IMailGatewayBuilder _mailGatewayBuilder;

    public ReportManager(IMemoryRepository<Employee> employees, IEmployeeReport report, IMailGatewayBuilder mailGatewayBuilder, ILogger<IReportManager> logger)
    {
        _employees = employees;
        _report = report;
        _mailGatewayBuilder = mailGatewayBuilder;
        _logger = logger;
    }

    public void PrintEmployeesReports()
    {
        foreach (var item in _employees.GetAll())
        {
            Console.WriteLine("{0} {1}", item.Name, item.Email);
            foreach (var order in item.Orders)
            {
                Console.WriteLine("\t{0,-20} {1,10} {2,10} {3,10}", order.Product.Name, order.Product.Price, order.Count, order.Total);
            }
        }
    }

    public async Task SendEmployeesReportsAsync(CancellationToken cancel)
    {
        using var mailGateway = await _mailGatewayBuilder.BuildAsync(cancel).ConfigureAwait(false);

        foreach (var item in _employees.GetAll())
        {
            await SendEmployeeReportAsync(item, mailGateway, cancel);
        }
    }

    private async Task SendEmployeeReportAsync(Employee employee, IGateway mailGateway, CancellationToken cancel = default)
    {
        _logger.LogInformation("Отправка отчета по пользователю {0}", employee.Name);
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        string report = await _report.CreateAsync(employee, cancel).ConfigureAwait(false);

        var message = new Message
        {
            Body = report,
            Subject = "Заказы сотрудника",
            IsHtml = true,
            Name = "Сервис отправки сообщений",
            To = "orlfi@mail.ru"
        };

        await mailGateway.SendAsync(message, cancel);
    }

}
