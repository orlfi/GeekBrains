using LibraryService.Web.DAL.Interfaces;
using LibraryService.Web.Models;

namespace LibraryService.Web.DAL;

public class LibraryRepository : ILibraryRepository
{
    private readonly ILibraryContext _db;

    public LibraryRepository(ILibraryContext db)
    {
        _db = db;
    }

    public IEnumerable<Book> GetAll()
    {
        return _db.Books;
    }

    public IEnumerable<Book> GetByTitle(string title)
    {
        return _db.Books.Where(b => b.Title.ToLower().Contains(title.ToLower()));
    }

    public IEnumerable<Book> GetByAuthor(string author)
    {
        return _db.Books.Where(b => b.Authors.Any(a => a.Name.ToLower().Contains(author.ToLower())));
    }

    public IEnumerable<Book> GetByCategory(string category)
    {
        return _db.Books.Where(b => b.Category.ToLower().Contains(category.ToLower()));
    }

    public Book GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));

        var result = _db.Books.SingleOrDefault(b => b.Id == id);

        if (result is null)
            throw new NullReferenceException($"Книга с id = {id} не найдена");

        return result;
    }

    public string Add(Book entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        entity.Id = Guid.NewGuid().ToString();
        _db.Books.Add(entity);

        return entity.Id;
    }

    public void Update(Book entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        if (string.IsNullOrEmpty(entity.Id))
            throw new NullReferenceException(nameof(entity.Id));

        var book = GetById(entity.Id);
        var index = _db.Books.IndexOf(book);
        _db.Books[index] = entity;
    }

    public void Delete(Book entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        if (string.IsNullOrEmpty(entity.Id))
            throw new NullReferenceException(nameof(entity.Id));

        var book = GetById(entity.Id);
        _db.Books.Remove(book);
    }
}