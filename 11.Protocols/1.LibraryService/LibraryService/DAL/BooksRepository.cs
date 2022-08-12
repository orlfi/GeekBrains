using LibraryService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryService.DAL
{
    public sealed class BooksRepository : IBooksRepository
    {
        private readonly ILibraryContext _db;

        public BooksRepository(ILibraryContext db)
        {
            _db = db;
        }

        public IEnumerable<Models.Book> GetAll()
        {
            return _db.Books.ToArray();
        }

        public Models.Book GetById(string id)
        {
            var result = _db.Books.SingleOrDefault(item => item.Id == id);

            if (result == null)
                throw new NullReferenceException(string.Format("Запись с {0} не найдена", id));

            return result;
        }

        public IEnumerable<Models.Book> GetByTitle(string title)
        {
            var result = _db.Books.Where(item => item.Title.ToLower().Contains(title.ToLower())).ToArray();
            return result;
        }

        public IEnumerable<Models.Book> GetByCategory(string category)
        {
            var result = _db.Books.Where(item => item.Category.ToLower().Contains(category.ToLower())).ToArray();
            return result;
        }

        public IEnumerable<Models.Book> GetByAuthor(string author)
        {
            var result = _db.Books.Where(b => b.Authors.Any(a => a.Name.ToLower().Contains(author.ToLower())));
            return result;
        }

        public void Add(Models.Book entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = Guid.NewGuid().ToString();
            _db.Books.Add(entity);
        }

        public void Update(Models.Book entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (string.IsNullOrEmpty(entity.Id))
                throw new NullReferenceException("Id не задан");

            var book = GetById(entity.Id);
            var index = _db.Books.IndexOf(book);
            _db.Books[index] = entity;
        }

        public void Delete(Models.Book entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (string.IsNullOrEmpty(entity.Id))
                throw new NullReferenceException("Id не задан");

            var book = GetById(entity.Id);
            _db.Books.Remove(book);
        }
    }
}