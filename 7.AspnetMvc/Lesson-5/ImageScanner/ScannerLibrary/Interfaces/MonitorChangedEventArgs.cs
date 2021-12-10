namespace ScannerLibrary.Interfaces;

public class MonitorChangedEventArgs : EventArgs
{
    public IMonitorData? Data { get; set; } = null;
}
