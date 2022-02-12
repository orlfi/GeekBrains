namespace ScannerLibrary.Interfaces;

public interface IScannerContext
{
    void Run(string fileName);

    void ConfigureProcessor(IScanSaver saver);
}
