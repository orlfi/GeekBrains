using LibraryService.Web.Models;

namespace LibraryService.Web.DAL.Interfaces;

public interface ILibraryContext
{
    IList<Book> Books { get; }
}
