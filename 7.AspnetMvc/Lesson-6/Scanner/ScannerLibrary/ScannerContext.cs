using Microsoft.Extensions.Logging;
using ScannerLibrary.Interfaces;
using ScannerLibrary.Savers;

namespace ScannerLibrary;

public class ScannerContext : IScannerContext
{
    private readonly IScannerDevice? _device;

    private readonly ILogger? _logger;

    private IScanSaver? _saver;

    public ScannerContext(IScannerDevice device, IScanStrategyResolver scanStrategyResolver, ScanStrategyName strategyName, ILogger<ScannerContext> logger)
    {
        _device = device;
        _logger = logger;
        _saver = scanStrategyResolver.GetScanSaverByName(strategyName.Name??"");

        if (_logger is not null)
            device.MonitorChanged += (sender, eventArgs) => _logger.LogInformation(eventArgs.Data?.ToString() ?? "");
    }

    public void ConfigureProcessor(IScanSaver saver)
    {
        _saver = saver;
    }

    public void Run(string fileName)
    {
        if (_device is null)
            throw new ArgumentNullException("Сканер не найден");

        if (_saver is null)
            throw new ArgumentNullException("Обработчик не найден");

        _logger?.LogInformation("Сохранение в формат в {0}", _saver.GetType());
        _saver.ScanAndSave(_device, fileName);
    }
}
