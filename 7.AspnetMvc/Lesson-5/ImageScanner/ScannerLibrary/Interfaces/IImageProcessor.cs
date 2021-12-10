namespace ScannerLibrary.Interfaces;

public interface IImageProcessor
{
    void Execute(byte[] data, string fileName);
}
