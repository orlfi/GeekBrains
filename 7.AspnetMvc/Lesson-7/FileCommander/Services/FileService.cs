using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using FileCommander.Reports.Interfaces;

namespace FileCommander.Services;

public class FileService
{
    private readonly IReportResolver _reportResolver;

    public FileService(IReportResolver reportResolver)
    {
        _reportResolver = reportResolver;

    }

    public void CreateReport(string fileName)
    {
        string reportFileName = "test.docx";
        var report = _reportResolver.GetReportByFileName(fileName);
    }
}
