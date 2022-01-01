namespace FileCommander.Reports.Interfaces;

public interface IReport
{
    void MakeReport(string reportFileName, string sourceFileName, CreateReportOptions options);
}
