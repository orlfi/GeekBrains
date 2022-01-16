using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;
using BankCards.DAL.Context;
using BankCards.Domain;
using BankCards.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankCards.DAL.Repositories
{
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

        public async Task<Card?> GetAsync(int id, CancellationToken cancel = default)
        {
            var result = await _set.SingleOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);
            return result;
        }

        public async Task<int> CreateAsync(Card entity, CancellationToken cancel = default)
        {
            await _set.AddAsync(entity, default).ConfigureAwait(false);
            await _db.SaveChangesAsync(default).ConfigureAwait(false);
            _logger.LogInformation("Новая карта добавлена в БД");
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Card entity, CancellationToken cancel = default)
        {
            _db.Entry<Card>(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync(default).ConfigureAwait(false);
            _logger.LogInformation("Данные по карте изменены {0}", entity.Number);
            return true;
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

}