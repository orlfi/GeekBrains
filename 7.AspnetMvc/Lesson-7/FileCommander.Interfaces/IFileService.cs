namespace FileCommander.Interfaces;

public interface IFileService
{
    Task CopyFileAsync(string source, string destination, IProgress<int>? progress = null, CancellationToken cancel = default);
    void CreateReport(string reportFileName, string sourceFileName);
}
