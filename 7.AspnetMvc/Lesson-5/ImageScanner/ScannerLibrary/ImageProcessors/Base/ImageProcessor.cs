using ScannerLibrary.Interfaces;

namespace ScannerLibrary.ImageProcessors.Base;

public abstract class ImageProcessor : IImageProcessor
{
    public abstract void Rotate(IPicture picture, double angle);
    public abstract void Flip(IPicture picture, FlipMode flipMode);
}
