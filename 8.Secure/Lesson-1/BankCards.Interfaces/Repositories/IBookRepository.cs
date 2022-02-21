using BankCards.Domain;
using BankCards.Domain.Mongo;

namespace BankCards.Interfaces.Repositories;

public interface IBookRepository : IMongoRepository<Book>
{
}
