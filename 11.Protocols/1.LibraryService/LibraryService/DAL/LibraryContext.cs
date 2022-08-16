using LibraryService.DAL.Interfaces;
using LibraryService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LibraryService.DAL
{
    public sealed class LibraryContext: ILibraryContext
    {
        private readonly IList<Book> _books;

        public IList<Models.Book> Books
        {
            get
            {
                return _books;
            }
        }

        public LibraryContext()
        {
            _books = JsonConvert.DeserializeObject<List<Book>>(Encoding.UTF8.GetString(Properties.Resources.books)); 
        }
    }
}