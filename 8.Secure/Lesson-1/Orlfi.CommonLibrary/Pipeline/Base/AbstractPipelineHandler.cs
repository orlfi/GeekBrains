using Orlfi.CommonLibrary.Pipeline.Interfaces;

namespace Orlfi.CommonLibrary.Pipeline.Base;

public abstract class AbstractPipelineHandler<T> : IPipelineHandler<T>
{
    private IPipelineHandler<T> _next = default!;

    public IPipelineHandler<T> SetNext(IPipelineHandler<T> handler)
    {
        _next = handler;
        return handler;
    }

    public void Run(IPipelineContext<T> context)
    {
        var result = Handle(context);
        if (result)
            _next?.Run(context);
    }

    protected abstract bool Handle(IPipelineContext<T> context);
}
