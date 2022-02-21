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
            _next.Run(context);
    }

    public async Task RunAsync(IPipelineContext<T> context, CancellationToken cancel = default)
    {
        var result = await HandleAsync(context, cancel).ConfigureAwait(false);

        if (result && _next is not null)
            await _next.RunAsync(context, cancel).ConfigureAwait(false);
    }


    protected abstract bool Handle(IPipelineContext<T> context);

    protected abstract Task<bool> HandleAsync(IPipelineContext<T> context, CancellationToken cancel = default);
}
