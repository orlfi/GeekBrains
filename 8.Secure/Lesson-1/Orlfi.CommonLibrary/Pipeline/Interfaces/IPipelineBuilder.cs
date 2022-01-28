namespace Orlfi.CommonLibrary.Pipeline.Interfaces;

public interface IPipelineBuilder<T>
{
    public IPipelineBuilder<T> Use(IPipelineHandler<T> handler);
    public IPipelineHandler<T> Build();
}
