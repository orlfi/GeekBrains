using MailService.Domain.Entities;
using ReportSender.Domain.Entities;

namespace ReportSender.Interfaces.Reports;

public interface IEmployeeReport
{
    string TemplateFileName { get; init; }

    Task<string> CreateAsync(Employee employee, CancellationToken cancel = default);
}
