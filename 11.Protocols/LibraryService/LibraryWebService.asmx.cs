using LibraryService.DAL;
using LibraryService.DAL.Interfaces;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LibraryService
{
    /// <summary>
    /// Summary description for LibraryWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LibraryWebService : System.Web.Services.WebService
    {
        private readonly IBooksRepository _repository;

        public LibraryWebService()
        {
            _repository = new BooksRepository(new LibraryContext());
        }

        [WebMethod]
        public List<Book> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        [WebMethod]
        public List<Book> GetByTitle(string title)
        {
            return _repository.GetByTitle(title).ToList();
        }

        [WebMethod]
        public List<Book> GetByCategory(string category)
        {
            return _repository.GetByCategory(category).ToList();
        }

        [WebMethod]
        public List<Book> GetByAuthor(string author)
        {
            return _repository.GetByAuthor(author).ToList();
        }

        [WebMethod]
        public void Add(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("Book");
            
            _repository.Add(book);
        }
        
        [WebMethod]
        public void Update(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("Book");

            _repository.Update(book);
        }

        [WebMethod]
        public void Delete(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("Book");

            _repository.Delete(book);
        }
    }
}
