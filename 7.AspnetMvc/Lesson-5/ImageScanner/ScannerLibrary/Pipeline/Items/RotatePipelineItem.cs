using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScannerLibrary.Interfaces;
using ScannerLibrary.Pipeline.Base;

namespace ScannerLibrary.Pipeline.Items;

public class RotatePipelineItem : PipelineItem<IPicture>, IPicturePipelineItem
{
    private readonly IImageProcessor _processor;
    private readonly double _angle;

    public RotatePipelineItem(IImageProcessor processor, double angle)
    {
        _processor = processor;
        _angle = angle;
    }

    public override bool Process(IPicture data)
    {
        _processor.Rotate(data, _angle);
        return true;
    }
}
