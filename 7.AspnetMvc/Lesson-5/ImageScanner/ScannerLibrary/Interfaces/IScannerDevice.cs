using System;

namespace ScannerLibrary.Interfaces;

public interface IScannerDevice
{
    event EventHandler<NewScanEventArgs>? NewScanEvent;

    void Scan();
}
