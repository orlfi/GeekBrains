using LibraryService.Web.DAL.Interfaces;
using LibraryService.Web.Models;
using System.Text.Json;

namespace LibraryService.Web.DAL;

public class LibraryContext : ILibraryContext
{
    private IList<Book> _books = default!;
    public IList<Book> Books => _books;

    public LibraryContext()
    {
        Initialize();
    }

    private void Initialize()
    {
        using var reader = new FileStream("books.json", FileMode.Open);
        _books = JsonSerializer.Deserialize<List<Book>>(reader, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}