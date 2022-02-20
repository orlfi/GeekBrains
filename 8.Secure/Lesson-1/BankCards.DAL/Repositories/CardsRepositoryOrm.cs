using System.Net;
using BankCards.DAL.Context;
using BankCards.Domain;
using BankCards.Exceptions;
using BankCards.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankCards.DAL.Repositories;

public class CardsRepositoryOrm : ICardRepository
{
    private readonly BankContext _db;
    private readonly ILogger<CardsRepositoryOrm> _logger;

    private DbSet<Card> _set => _db.Cards;

    public CardsRepositoryOrm(BankContext db, ILogger<CardsRepositoryOrm> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<Card>> GetAllAsync(CancellationToken cancel = default)
    {
        var cards = await _set.ToArrayAsync(cancel).ConfigureAwait(false);
        return cards;
    }

    public async Task<Card?> GetByIdAsync(int id, CancellationToken cancel = default)
    {
        var result = await _set.SingleOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Возвращает карты по совпадению части номера
    /// EF функции sql inject безопасны
    /// </summary>
    public async Task<IEnumerable<Card>> GetByNumberAsync(string numberPattern, CancellationToken cancel = default)
    {
        var result = await _set.Where(item => EF.Functions.Like(item.Number, numberPattern)).ToArrayAsync(cancel).ConfigureAwait(false);
        return result;
    }

    public async Task<int> CreateAsync(Card entity, CancellationToken cancel = default)
    {
        await _set.AddAsync(entity, cancel).ConfigureAwait(false);
        await _db.SaveChangesAsync(default).ConfigureAwait(false);
        _logger.LogInformation("Новая карта добавлена в БД");
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Card entity, CancellationToken cancel = default)
    {
        try
        {
            _db.Entry<Card>(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync(default).ConfigureAwait(false);
            _logger.LogInformation("Данные по карте изменены {0}", entity.Number);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при изменении банковской карты {0}", entity.Id);
            throw new DbException(HttpStatusCode.BadRequest, "Ошибка БД");
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancel = default)
    {
        var entity = await _set.SingleOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);
        if (entity is null)
        {
            _logger.LogInformation("Удаляемая карта {0} не найдена", id);
            return false;
        }

        _db.Entry<Card>(entity).State = EntityState.Deleted;
        await _db.SaveChangesAsync(default).ConfigureAwait(false);
        _logger.LogInformation("Карта {0} удалена", entity.Number);
        return true;
    }
}
