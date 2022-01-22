namespace BankCards.Interfaces;

public interface IDbInitializer
{
    Task InitializeAsync(CancellationToken cancel = default);
}
