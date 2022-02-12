using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using FileCommander.Reports.Interfaces;

namespace FileCommander.Services;

public class FileService
{
    private readonly IReportResolver _reportResolver;

    public FileService(IReportResolver reportResolver)
    {
        _reportResolver = reportResolver;

    }

    public void CreateReport(string reportFileName, string sourceFileName)
    {
        reportFileName = "test.docx";
        var report = _reportResolver.GetReportByFileName(sourceFileName);
        report.MakeReport(reportFileName, sourceFileName);
    }
}
