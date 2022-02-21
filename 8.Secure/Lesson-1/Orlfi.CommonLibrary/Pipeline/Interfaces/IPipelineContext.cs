namespace Orlfi.CommonLibrary.Pipeline.Interfaces;

public interface IPipelineContext<T>
{
    public T Data { get; set; }
}
