using System;

namespace ScannerLibrary.Interfaces;

public interface IScannerDevice
{
    event EventHandler<MonitorChangedEventArgs> MonitorChanged;

    Stream Scan();
}
 