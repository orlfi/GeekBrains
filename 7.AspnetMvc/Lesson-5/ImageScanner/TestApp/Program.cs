using ScannerLibrary;
using ScannerLibrary.Devices;
using ScannerLibrary.ImageProcessors;
using ScannerLibrary.ImageProcessors.Base;
using ScannerLibrary.Interfaces;
using ScannerLibrary.Pipeline;
using ScannerLibrary.Pipeline.Items;

try
{
    var processor = new ImageSharpProcessor();
    var context = ScannerContextBuilder.Create(new FileScannerDevice(Directory.GetCurrentDirectory()), Directory.GetCurrentDirectory() + "\\out")
        .WithImageProcessorPipeline(PicturePipeline.Create(PipelineConfiguration<IPicturePipelineItem, IPicture>
            .Create()
            .With(new RotatePipelineItem(processor, 45))
            .With(new FlipPipelineItem(processor, FlipMode.Horizontal))))
        .Build();

    context.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.ReadLine();