using Orlfi.CommonLibrary.Pipeline.Interfaces;

namespace Orlfi.CommonLibrary.Pipeline.Base
{
    public class PipelineContext<T> : IPipelineContext<T>
    {
        public T Data { get; set; } = default!;

        public PipelineContext(T data) => Data = data;
    }
}