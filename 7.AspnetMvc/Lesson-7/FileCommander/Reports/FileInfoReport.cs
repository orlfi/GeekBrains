using System.Diagnostics;
using System.IO;
using FileCommander.Reports.Interfaces;
using TemplateEngine.Docx;

namespace FileCommander.Reports;

public class FileInfoReport : IReport
{
    public const string reportTemplate = "FileReportTemplate.docx";

    public void MakeReport(string reportFileName, string sourceFileName, CreateReportOptions options)
    {
        FileInfo fi = new FileInfo(sourceFileName);
        if (!fi.Exists)
            throw new FileNotFoundException(sourceFileName);

        fi = new FileInfo(reportFileName);
        if (fi.Exists)
            fi.Delete();

        File.Copy(reportTemplate, reportFileName);

        var valuesToFill = new Content(
            new FieldContent("FileName", fi.Name),
            new FieldContent("FilePath", Path.GetDirectoryName(fi.FullName)),
            new FieldContent("FileSize", fi.Length.ToString()),
            new FieldContent("ReadOnlyAttribute", fi.Attributes.HasFlag(FileAttributes.ReadOnly) ? "true" : "false"),
            new FieldContent("HiddenAttribute", fi.Attributes.HasFlag(FileAttributes.Hidden) ? "true" : "false"),
            new FieldContent("ArchiveAttribute", fi.Attributes.HasFlag(FileAttributes.Archive) ? "true" : "false"),
            new FieldContent("SystemAttribute", fi.Attributes.HasFlag(FileAttributes.System) ? "true" : "false")
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
