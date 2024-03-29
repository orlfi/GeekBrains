namespace Orlfi.CommonLibrary.Pipeline.Interfaces;

public interface IPipelineHandler<T>
{
    IPipelineHandler<T> SetNext(IPipelineHandler<T> handler);

    void Run(IPipelineContext<T> context);

    Task RunAsync(IPipelineContext<T> context, CancellationToken cancel = default);
}
