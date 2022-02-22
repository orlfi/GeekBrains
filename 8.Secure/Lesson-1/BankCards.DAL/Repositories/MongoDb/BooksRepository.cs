using BankCards.Domain.Mongo;
using BankCards.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BankCards.DAL.Repositories.MongoDb;

public class BooksRepository : IBookRepository
{
    
    private readonly IMongoCollection<Book> _books;
    private readonly ILogger<BooksRepository> _logger;

    public BooksRepository(IMongoDatabase db, ILogger<BooksRepository> logger)
    {
        _books = db.GetCollection<Book>("Books");
        _logger = logger;
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancel = default)
    {
        var result = await _books.Find(_ => true).ToListAsync(cancel).ConfigureAwait(false);
        return result;
    }

    public async Task<Book?> GetByIdAsync(string id, CancellationToken cancel = default)
    {
        var result = await _books.Find(item => item.Id == id).FirstOrDefaultAsync(cancel).ConfigureAwait(false);
        return result;
    }


    public async  Task<string> CreateAsync(Book entity, CancellationToken cancel = default)
    {
        await _books.InsertOneAsync(entity, cancellationToken:cancel).ConfigureAwait(false);
        _logger.LogInformation("Новая книга {0} добавлена в БД", entity);
        return entity.Id;
    }

    public async  Task<bool> UpdateAsync(Book entity, CancellationToken cancel = default)
    {
        var result = await _books.ReplaceOneAsync(item => item.Id == entity.Id, entity).ConfigureAwait(false);
        _logger.LogInformation("Данные по книге {0} изменены", entity);
        return true;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancel = default)
    {
        await _books.DeleteOneAsync(x => x.Id == id);
        _logger.LogInformation("Книга с Id {0} удалена", id);
        return true;
    }
}
