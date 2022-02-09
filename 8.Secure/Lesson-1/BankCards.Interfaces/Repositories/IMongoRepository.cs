using BankCards.Domain.Base;

namespace BankCards.Interfaces.Repositories;

public interface IMongoRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default);

    Task<T?> GetByIdAsync(string id, CancellationToken cancel = default);

    Task<string> CreateAsync(T entity, CancellationToken cancel = default);

    Task<bool> UpdateAsync(T entity, CancellationToken cancel = default);

    Task<bool> DeleteAsync(string id, CancellationToken cancel = default);
}
