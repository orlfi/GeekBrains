using Hardwares.DAL.Context;
using Hardwares.Interfaces.Common;
using Hardwares.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardwares.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly HardwaresDb _db;
    private readonly ILogger<Repository<T>> _logger;

    DbSet<T> Set { get; }

    IQueryable<T> Items => Set;

    public Repository(HardwaresDb db, ILogger<Repository<T>> logger)
    {
        _db = db;
        _logger = logger;
        Set = db.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(int id, CancellationToken token = default)
    {
        var result = await Items.ToArrayAsync().ConfigureAwait(false);
        return result;
    }

    public async Task<T> GetAsync(int id, CancellationToken token = default)
    {
        var result = await Items.SingleOrDefaultAsync(item => item.Id == id).ConfigureAwait(false);
        return result;
    }

    public async Task<int> AddAsync(T entity, CancellationToken token = default)
    {
        await Set.AddAsync(entity, token);
        _logger.LogInformation("Объект {0} добавлен в базу", entity);
        await _db.SaveChangesAsync(token).ConfigureAwait(false);
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken token = default)
    {
        _db.Entry<T>(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync(token).ConfigureAwait(false);
        _logger.LogInformation("Объект {0} изменен", entity);
        return true;
    }


    public async Task<bool> DeleteAsync(T entity, CancellationToken token = default)
    {
        var item = await Set.SingleOrDefaultAsync(item => item.Id == entity.Id, token).ConfigureAwait(false);
        if (item == null)
        {
            _logger.LogInformation("Удаляемый объект {0} не найден в базе", entity);
            return false;
        }
        _db.Entry<T>(entity).State = EntityState.Detached;
        await _db.SaveChangesAsync(token).ConfigureAwait(false);
        _logger.LogInformation("Объект {0} удален", entity);
        return true;
    }
}
