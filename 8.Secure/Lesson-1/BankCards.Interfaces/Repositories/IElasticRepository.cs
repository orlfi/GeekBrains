using BankCards.Domain.Mongo;

namespace BankCards.Interfaces.Repositories;

public interface IElasticRepository
{
    IEnumerable<Book> Search(string query);
}
