namespace ScannerLibrary.Interfaces
{
    public interface IPipelineItem<TData>
    {
        IPipelineItem<TData> Next { get; set; }

        bool Execute(TData data);

        bool Process(TData data);
    }
}