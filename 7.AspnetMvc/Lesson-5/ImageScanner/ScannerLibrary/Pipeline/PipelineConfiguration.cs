using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Pipeline;
public sealed class PipelineConfiguration<TItem, TData> where TItem : IPipelineItem<TData>
    //public sealed class PipelineConfiguration<TItem>
{
    private TItem? _first;

    private TItem? _last;

    private PipelineConfiguration() { }

    public static PipelineConfiguration<TItem, TData> Create()
    {
        return new PipelineConfiguration<TItem, TData>();
    }

    public PipelineConfiguration<TItem, TData> With(TItem item)
    {
        if (_first is null)
        {
            _first = item;
            _last = _first;
        }
        else
        {
            if (_last != null)
                _last.Next = item;

            _last = item;
        }
        return this;
    }

    public TItem Build()
    {
        if (_first is null)
            throw new NullReferenceException("Pipeline не может быть пустой");

        return _first;
    }
}