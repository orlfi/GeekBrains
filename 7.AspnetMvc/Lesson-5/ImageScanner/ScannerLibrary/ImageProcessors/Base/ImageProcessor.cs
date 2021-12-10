using ScannerLibrary.Interfaces;

namespace ScannerLibrary.ImageProcessors.Base;

public abstract class ImageProcessor : IImageProcessor
{
    public abstract void Execute(byte[] data, string fileName);
}
