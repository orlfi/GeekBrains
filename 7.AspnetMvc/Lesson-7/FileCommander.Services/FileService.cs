using FileCommander.Infrastructure.EventArguments;
using FileCommander.Interfaces;
using FileCommander.Interfaces.Reports;
using FileCommander.Services.Reports;
using Microsoft.Extensions.Logging;

namespace FileCommander.Services;

public class FileService : IFileService
{
    private const int DefaultBufferSize = 81920;

    private readonly IReportResolver _reportResolver;

    private readonly ILogger<FileService> _logger;

    public event EventHandler<ProgressArgs> Progress;

    public FileService(IReportResolver reportResolver, ILogger<FileService> logger)
    {
        _reportResolver = reportResolver;
        _logger = logger;
    }

    public void CreateReport(string reportFileName, string sourceFileName)
    {
        var report = _reportResolver.GetReportByFileName(sourceFileName);
        var options = new CreateReportOptions() {  OpenAfterCreate = true};
        report.MakeReport(reportFileName, sourceFileName, options);
    }

    public async Task CopyFileAsync(string source, string destination, IProgress<int>? progress = default, CancellationToken cancel = default)
    {
        cancel.ThrowIfCancellationRequested();
        using FileStream reader = File.OpenRead(source);
        int bufferSize = reader.Length < DefaultBufferSize ? Convert.ToInt32(reader.Length) : DefaultBufferSize;
        var buffer = new byte[bufferSize];
        using FileStream writer = File.OpenWrite(destination);
        int read = 0;
        long totalRead = 0;
        int calculatedProgress = 0;
        int currentProgress = 0;
        while ((read = await reader.ReadAsync(buffer, 0, bufferSize, cancel).ConfigureAwait(false)) > 0)
        {
            if (cancel.IsCancellationRequested)
            {
                writer.Close();
                File.Delete(destination);
                _logger.LogInformation("Отмена операции копирования файла {0} в {1}", source, destination);
                throw new OperationCanceledException();
            }
            await writer.WriteAsync(buffer, 0, read, cancel).ConfigureAwait(false);
            totalRead += read;
            calculatedProgress = (int)((double)totalRead / reader.Length * 100);
            if (currentProgress != calculatedProgress)
            {
                currentProgress = calculatedProgress;
                progress?.Report(currentProgress);
            }
        }
    }
}
