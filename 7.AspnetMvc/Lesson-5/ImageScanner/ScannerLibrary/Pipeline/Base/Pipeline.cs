using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Pipeline.Base;

public abstract class Pipeline<TItem, TData> : IPipeline<TData> where TItem : IPipelineItem<TData>
{
    protected TItem? _firstItem;

    public void Run(TData data)
    {
        _firstItem?.Execute(data);
    }

}