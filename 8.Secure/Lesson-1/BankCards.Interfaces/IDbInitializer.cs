namespace BankCards.Interfaces;

public interface IDbInitializer
{
    Task InitializeAsync(bool removeFirst = false, CancellationToken cancel = default);
}
