namespace Hardwares.Interfaces;

public interface IDbInitalizer
{
    Task Initialize(bool remove, CancellationToken cancel= default);
    Task RemoveAsync(CancellationToken cancel = default);
}
