using ScannerLibrary.Interfaces;
using ScannerLibrary.ImageProcessors.Base;
using SixLabors.ImageSharp;

namespace ScannerLibrary.ImageProcessors;

public class RotateProcessor : ImageProcessor
{
    private int _angle;

    public RotateProcessor(int angle) => _angle = angle;

    public override void Execute(byte[] data, string fileName)
    {
        var image = Image.Load(data);
            private byte[] RotateImage(byte[] imageInBytes)
    {
        using (var image = Image.Load(imageInBytes, out var imageFormat))
        {
            image.Mutate(x => x.Rotate(90));
            return ImageToByteArray(image, imageFormat);
        }
    }
    }
}
