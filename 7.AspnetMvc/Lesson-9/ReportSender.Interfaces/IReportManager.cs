namespace ReportSender.Interfaces;

public interface IReportManager
{
    void PrintEmployeesReports();
    Task SendEmployeesReportsAsync(CancellationToken cancel);
}
