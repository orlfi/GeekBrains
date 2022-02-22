using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScannerLibrary.ImageProcessors.Base;
using ScannerLibrary.Interfaces;
using ScannerLibrary.Pipeline.Base;

namespace ScannerLibrary.Pipeline.Items;

public class FlipPipelineItem : PipelineItem<IPicture>, IPicturePipelineItem
{
    private readonly IImageProcessor _processor;
    private FlipMode _flipMode;

    public FlipPipelineItem(IImageProcessor processor, FlipMode flipMode)
    {
        _processor = processor;
        _flipMode = flipMode;
    }

    public override bool Process(IPicture data)
    {
        _processor.Flip(data, _flipMode);
        return true;
    }
}
