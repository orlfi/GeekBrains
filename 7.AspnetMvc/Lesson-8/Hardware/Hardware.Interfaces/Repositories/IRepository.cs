using Hardwares.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardwares.Interfaces.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);

    Task<T> GetAsync(int id, CancellationToken token = default);

    Task<int> AddAsync(T entity, CancellationToken token = default);

    Task<bool> UpdateAsync(T entity, CancellationToken token = default);

    Task<bool> DeleteAsync(T entity, CancellationToken token = default);
}
