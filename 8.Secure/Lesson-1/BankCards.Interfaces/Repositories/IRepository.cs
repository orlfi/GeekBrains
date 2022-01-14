using BankCards.Domain;
using BankCards.Domain.Base;

namespace BankCards.Interfaces.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<T> GetAllAsync();

    Task<T> GetAsync(int id);

    Task<int> CreateAsync(T entity);

    Task<bool> UpdateAsync(T entity);

    Task<bool> DeleteAsync(int id);
}
