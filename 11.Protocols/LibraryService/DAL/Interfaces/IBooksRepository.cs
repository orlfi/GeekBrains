using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.DAL.Interfaces
{
    public interface IBooksRepository: IRepository<Book>
    {
        IEnumerable<Book> GetByTitle(string title);
        IEnumerable<Book> GetByCategory(string category);
        IEnumerable<Book> GetByAuthor(string author);
    }
}
