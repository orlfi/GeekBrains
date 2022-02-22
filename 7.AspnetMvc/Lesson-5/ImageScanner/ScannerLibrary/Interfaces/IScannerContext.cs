namespace ScannerLibrary.Interfaces;

public interface IScannerContext
{
    void ConfigureImageProcessorPipeline(IPicturePipeline imageProcessorPipeline);
    void Run();
}
