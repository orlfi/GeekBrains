namespace ScannerLibrary.Interfaces;

public class NewScanEventArgs : EventArgs
{
    public string? FileName { get; set; }
    public byte[]? Data { get; set; }
}
