using MailService.Domain.Entities;
using RazorEngine;
using RazorEngine.Templating;
using ReportSender.Interfaces.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSender.Interfaces.Reports;

public class EmployeeHtmlReport : IEmployeeReport
{
    private const string defaultTemplateFileName = "Templates\\EmployeeHtmlTemplate.cshtml";

    public string TemplateFileName { get; init; }

    public EmployeeHtmlReport()
    {
        TemplateFileName = defaultTemplateFileName;
    }

    public async Task<string> CreateAsync(Employee employee, CancellationToken cancel = default)
    {
        if (string.IsNullOrEmpty(TemplateFileName))
            throw new NullReferenceException("Не задан шаблон отчета");

        if (!File.Exists(TemplateFileName))
            throw new FileNotFoundException(TemplateFileName);

        string template = await File.ReadAllTextAsync(TemplateFileName, cancel).ConfigureAwait(false);
        var report = Engine.Razor.RunCompile(template, "EmployeeHtmlReport", null, employee);
        return report;
    }
}
