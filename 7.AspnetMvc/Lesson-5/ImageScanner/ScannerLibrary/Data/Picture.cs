using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Data;

public struct Picture : IPicture
{
    public byte[] Data { get; set; }
}
