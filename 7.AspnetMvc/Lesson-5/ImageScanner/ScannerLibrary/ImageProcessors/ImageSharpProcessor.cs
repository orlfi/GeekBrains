using ScannerLibrary.Interfaces;
using ScannerLibrary.ImageProcessors.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace ScannerLibrary.ImageProcessors;

public class ImageSharpProcessor : ImageProcessor
{
    private Image<Rgba32> FromPicture(IPicture picture, out IImageFormat imageFormat)
    {
        return Image.Load<Rgba32>(picture.Data, out imageFormat);
    }

    private byte[] ToByteArray(Image<Rgba32> image, IImageFormat imageFormat)
    {
        using var ms = new MemoryStream();
        image.Save(ms, imageFormat);
        return ms.ToArray();
    }

    public override void Flip(IPicture picture, Base.FlipMode flipMode)
    {
        using var image = FromPicture(picture, out var imageFormat);
        image.Mutate(x => x.Flip(SixLabors.ImageSharp.Processing.FlipMode.Horizontal));
        picture.Data = ToByteArray(image, imageFormat);
    }

    public override void Rotate(IPicture picture, double angle)
    {
        using var image = FromPicture(picture, out var imageFormat);
        image.Mutate(x => x.Rotate(45));
        picture.Data = ToByteArray(image, imageFormat);
    }
}
