using System.IO;
using Autofac.Features.Indexed;
using FileCommander.Reports.Interfaces;

namespace FileCommander.Services;

public class ReportResolver : IReportResolver
{
    private readonly IIndex<string, IReport> _reports;

    public ReportResolver(IIndex<string, IReport> reports) => _reports = reports;

    public IReport GetReportByFileName(string fileName)
    {
        if (Directory.Exists(fileName))
            return _reports["directory"];

        return _reports["file"];
    }
}
