namespace ScannerLibrary.Interfaces;

public interface IPipeline<TData>
{
    void Run(TData data);
}
