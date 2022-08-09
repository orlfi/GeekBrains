using LibraryService.Web.Models;

namespace LibraryService.Web.DAL.Interfaces;

public interface ILibraryRepository : IRepository<Book>
{
    IEnumerable<Book> GetByAuthor(string author);

    IEnumerable<Book> GetByCategory(string category);

    IEnumerable<Book> GetByTitle(string title);
}
