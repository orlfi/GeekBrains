using FileCommander.Interfaces.Reports;

namespace FileCommander.Services.Reports;

public class CreateReportOptions : ICreateReportOptions
{
    public bool OpenAfterCreate { get; init; } = false;
}
