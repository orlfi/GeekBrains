namespace ScannerLibrary.Interfaces;

public interface IScanSaver
{
    void ScanAndSave(IScannerDevice scanner, string fileName);
}
