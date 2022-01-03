using ReportSender.Interfaces.Base;
using ReportSender.Interfaces.Repositories;

namespace ReportSender.DAL.Repositories;

public class MemoryRepository<T> : IMemoryRepository<T> where T : IEntity
{
    private MemoryDatabase _db;

    protected IList<T> Set { get; }

    public MemoryRepository(MemoryDatabase db)
    {
        _db = db;
        Set = _db.Set<T>();
    }
    public T? Get(int id)
    {
        return Set.SingleOrDefault(item => item.Id == id);
    }

    public IEnumerable<T> GetAll()
    {
        return Set;
    }
}
