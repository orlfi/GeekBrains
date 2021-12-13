using ScannerLibrary.ImageProcessors.Base;

namespace ScannerLibrary.Interfaces;

public interface IImageProcessor
{
    void Flip(IPicture picture, FlipMode flipMode);
    void Rotate(IPicture picture, double angle);
}
