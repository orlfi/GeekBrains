using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Pipeline.Base;

public abstract class PipelineItem<TData> : IPipelineItem<TData>
{
    public IPipelineItem<TData>? Next { get; set; }

    public bool Execute(TData data)
    {
        if (!Process(data) || Next is null)
            return false;

        return Next.Execute(data);
    }

    public abstract bool Process(TData data);
}
