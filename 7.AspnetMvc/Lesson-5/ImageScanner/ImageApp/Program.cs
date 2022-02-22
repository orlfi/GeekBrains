// See https://aka.ms/new-console-template for more information
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
Convert();
Console.WriteLine("Hello, World!");
static void Convert()
{
    var data = File.ReadAllBytes("1.bmp");
    using var image = Image.Load<Rgba32>(data, out var imageFormat);
    RotateImage(image);
    FlipImage(image);
    File.WriteAllBytes("2.bmp", ImageToByteArray(image, imageFormat));
}

static void RotateImage(Image<Rgba32> image)
{
    image.Mutate(x => x.Rotate(45));
}

static void FlipImage(Image<Rgba32> image)
{
    image.Mutate(x => x.Flip(FlipMode.Horizontal));
}

static byte[] ImageToByteArray(Image<Rgba32> image, IImageFormat imageFormat)
{
    using var ms = new MemoryStream();
    image.Save(ms, imageFormat);
    return ms.ToArray();
}
