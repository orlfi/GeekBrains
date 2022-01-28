namespace BankCards.ConsoleTests.Pipeline;

public class TestResult
{
    public IEnumerable<string> RuntimeLibraryInfo { get; set; } = null!;
    public IEnumerable<string> WebApiInfo { get; set; } = null!;
    public IEnumerable<string> DbInfo { get; set; } = null!;
}
