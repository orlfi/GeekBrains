namespace FileCommander.Interfaces.Reports;

public interface IReportResolver
{
    IReport GetReportByFileName(string fileName);
}
