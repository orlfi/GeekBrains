using ScannerLibrary.Interfaces;

namespace ScannerLibrary;

public class ScannerContext : IScannerContext
{
    private readonly IScannerDevice? _device;

    private readonly ILogger? _logger;

    private IScanSaver? _saver;

    private ScannerContext() { }

    internal ScannerContext(IScannerDevice device, ILogger? logger = null)
    {
        _device = device;
        _logger = logger;

        if (_logger is not null)
            device.MonitorChanged += (sender, eventArgs) => _logger.Log(eventArgs.Data?.ToString() ?? "");
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

        _saver.ScanAndSave(_device, fileName);
    }
}
