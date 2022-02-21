namespace FileCommander.Interfaces.Reports;

public interface IReport
{
    void MakeReport(string reportFileName, string sourceFileName, ICreateReportOptions options);
}
