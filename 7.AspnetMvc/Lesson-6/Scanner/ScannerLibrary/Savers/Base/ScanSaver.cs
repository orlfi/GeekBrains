using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Savers.Base;

public abstract class ScanSaver : IScanSaver
{
    public abstract void ScanAndSave(IScannerDevice device, string fileName);
}
