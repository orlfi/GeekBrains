using System.Collections;

namespace LibraryService.Web.DAL.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();

    T GetById(string id);

    string Add(T entity);

    void Update(T entity);

    void Delete(T entity);
}