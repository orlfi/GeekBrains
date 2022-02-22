using BankCards.Domain;

namespace BankCards.Interfaces.Repositories;

public interface ICardRepository : IRepository<Card>
{
    Task<IEnumerable<Card>> GetByNumberAsync(string number, CancellationToken cancel = default);
}
