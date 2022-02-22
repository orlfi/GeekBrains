using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScannerLibrary.Interfaces;
using ScannerLibrary.Pipeline.Base;

namespace ScannerLibrary.Pipeline;

public class PicturePipeline : Pipeline<IPicturePipelineItem, IPicture>, IPicturePipeline
{
    public static IPicturePipeline Create(PipelineConfiguration<IPicturePipelineItem, IPicture> configuration)
        => new PicturePipeline(configuration);

    private PicturePipeline(PipelineConfiguration<IPicturePipelineItem, IPicture> configuration)
    {
        _firstItem = configuration.Build();
    }
}