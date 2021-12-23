using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileCommander.Reports.Interfaces;
using TemplateEngine.Docx;

namespace FileCommander.Reports
{
    public class FileInfoReport : IReport
    {
        public const string reportTemplate = "FileInfoTemplate.docx";
        public void MakeReport(string reportName, string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            File.Copy(reportTemplate, reportName);

            var valuesToFill = new Content(
                new FieldContent("FileName", fi.Name),
                new FieldContent("FilePath", Path.GetDirectoryName(fi.FullName)),
                new FieldContent("FileSize", fi.Length.ToString()),
                new FieldContent("ReadOnlyAttribute", fi.Attributes.HasFlag(FileAttributes.ReadOnly) ? "true" : "false"),
                new FieldContent("HiddenAttribute", fi.Attributes.HasFlag(FileAttributes.Hidden) ? "true" : "false")
            );

            using (var outputDocument = new TemplateProcessor(reportName)
                .SetRemoveContentControls(true))
            {
                outputDocument.FillContent(valuesToFill);
                outputDocument.SaveChanges();
            }
        }
    }
}