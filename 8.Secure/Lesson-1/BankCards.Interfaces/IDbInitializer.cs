namespace BankCards.Interfaces;

public interface IDbInitializer
{
    Task InitializeAsync(bool removeFirst = false, bool initializeDatabaseWithTestData = false, CancellationToken cancel = default);
}
