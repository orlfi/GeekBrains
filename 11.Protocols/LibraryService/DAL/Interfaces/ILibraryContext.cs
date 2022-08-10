using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.DAL.Interfaces
{
    public interface ILibraryContext
    {
        IList<Book> Books { get; }
    }
}
