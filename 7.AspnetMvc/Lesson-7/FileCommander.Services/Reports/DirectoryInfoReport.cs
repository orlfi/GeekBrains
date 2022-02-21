using System.Diagnostics;
using FileCommander.Interfaces.Reports;
using TemplateEngine.Docx;

namespace FileCommander.Services.Reports;

public class DirectoryInfoReport : IReport
{
    public const string reportTemplate = "DirectoryReportTemplate.docx";

    public void MakeReport(string reportFileName, string sourceDirectoryName, ICreateReportOptions options)
    {
        var di = new DirectoryInfo(sourceDirectoryName);
        if (!di.Exists)
            throw new DirectoryNotFoundException(sourceDirectoryName);

        var fi = new FileInfo(reportFileName);
        if (fi.Exists)
            fi.Delete();

        File.Copy(reportTemplate, reportFileName);

        var tableContent = new TableContent("Files");
        foreach (var fileInfo in di.GetFiles())
        {
            tableContent.AddRow(
                new FieldContent("FileName", fileInfo.Name),
                new FieldContent("FileSize", fileInfo.Length.ToString()));
        }

        var valuesToFill = new Content(
        new FieldContent("DirectoryName", di.Name),
        new FieldContent("DirectoryPath", di.Parent.FullName),
        tableContent
    );

        using (var outputDocument = new TemplateProcessor(reportFileName)
            .SetRemoveContentControls(true))
        {
            outputDocument.FillContent(valuesToFill);
            outputDocument.SaveChanges();
        }

        if (options.OpenAfterCreate)
        {
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = reportFileName,
                UseShellExecute = true
            };

            Process.Start(info);
        }
    }
}
