using ReportSender.Interfaces.Base;

namespace ReportSender.Interfaces.Repositories
{
    public interface IMemoryRepository<T> where T : IEntity
    {
        T? Get(int id);
        IEnumerable<T> GetAll();
    }
}