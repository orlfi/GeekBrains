namespace ScannerLibrary.Interfaces;

public interface IScannerContext
{
    void ConfigureProcessor(IScanSaver saver);

    void Run(string fileName);
}
