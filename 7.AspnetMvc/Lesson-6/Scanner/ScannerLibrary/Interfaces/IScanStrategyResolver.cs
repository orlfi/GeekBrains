namespace ScannerLibrary.Interfaces;

public interface IScanStrategyResolver
{
    IScanSaver GetScanSaverByName(string name);
}

