using Orlfi.CommonLibrary.Pipeline.Interfaces;

namespace Orlfi.CommonLibrary.Pipeline.Base;

public class PipelineBuilder<T> : IPipelineBuilder<T>
{
    private IPipelineHandler<T> _first = null!;
    private IPipelineHandler<T> _last = null!;

    public static IPipelineBuilder<TData> Create<TData>() => new PipelineBuilder<TData>();

    public IPipelineBuilder<T> Use(IPipelineHandler<T> handler)
    {
        _ = handler ?? throw new ArgumentNullException(nameof(handler));

        if (_first is null)
        {
            _first = handler;
            _last = _first;
        }
        else
        {
            _last.SetNext(handler);
            _last = handler;
        }

        return this;
    }

    public IPipelineHandler<T> Build()
    { 
        if (_first is null)
            throw new NullReferenceException("Цепочка не содержит обработчиков");

        return _first;
    }
}
