using BankCards.Domain.Base;

namespace BankCards.Interfaces.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default);

    Task<T?> GetAsync(int id, CancellationToken cancel = default);

    Task<int> CreateAsync(T entity, CancellationToken cancel = default);

    Task<bool> UpdateAsync(T entity, CancellationToken cancel = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancel = default);
}
