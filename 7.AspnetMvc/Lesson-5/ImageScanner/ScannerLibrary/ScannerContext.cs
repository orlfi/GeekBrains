using ScannerLibrary.Data;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary;

public class ScannerContext : IScannerContext
{
    private readonly IScannerDevice? _device;

    private readonly string _outputDirectory;

    private IPicturePipeline? _imageProcessorPipeline;

    internal ScannerContext(IScannerDevice device, string outputDirectory)
    {
        _device = device;
        _outputDirectory = outputDirectory;

        _device.NewScanEvent += (sender, eventArgs) =>
        {
            _imageProcessorPipeline?.Run(eventArgs.Picture);
            Save(eventArgs.FileName, eventArgs.Picture);
        };
    }

    public void ConfigureImageProcessorPipeline(IPicturePipeline imageProcessorPipeline)
    {
        _imageProcessorPipeline = imageProcessorPipeline;
    }

    public void Run()
    {
        if (_device is null)
            throw new ArgumentNullException("Сканер не найден");

        if (_imageProcessorPipeline is null)
            throw new ArgumentNullException("Обработчик не настроен");

        _device.Scan();
    }

    private void Save(string fileName, IPicture picture)
    {
        if (!Directory.Exists(_outputDirectory))
            Directory.CreateDirectory(_outputDirectory);

        File.WriteAllBytes(Path.Combine(_outputDirectory, fileName), picture.Data);
    }
}
