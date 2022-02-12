namespace FileCommander.Reports.Interfaces;

public interface IReportResolver
{
    IReport GetReportByFileName(string fileName);
}
