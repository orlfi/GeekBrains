using MailService.Interfaces;
using MailService.Interfaces.Common;

namespace MailService.DAL.Repositories
{
    public interface IMemoryRepository<T> where T : IEntity
    {
        T? Get(int id);
        IEnumerable<T> GetAll();
    }
}