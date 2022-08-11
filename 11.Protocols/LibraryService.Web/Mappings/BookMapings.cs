using LibraryService.Web.Models;
using LibraryService.Web.ViewModels;
using LibraryServiceReference;

namespace LibraryService.Web.Mappings;

public static class BookMappings
{
    public static BookViewModel ToView(this Book book)
    {
        return new BookViewModel()
        {
            Id = book.Id,
            AgeLimit = book.AgeLimit,
            Authors = string.Join(", ", book.Authors.Select(item => $"{item.Name} ({item.Lang})")),
            Category = book.Category,
            Lang = book.Lang,
            Pages = book.Pages,
            PublicationDate = book.PublicationDate,
            Title = book.Title
        };
    }

    public static IEnumerable<BookViewModel> ToView(this IEnumerable<Book> books)
        => books.Select(item => item.ToView());

    public static Book ToModel(this BookViewModel bookView)
        => new()
        {
            Id = bookView.Id,
            AgeLimit = bookView.AgeLimit,
            Authors = Array.Empty<Author>(),
            Category = bookView.Category,
            Lang = bookView.Lang,
            Pages = bookView.Pages,
            PublicationDate = bookView.PublicationDate,
            Title = bookView.Title
        };

    public static IEnumerable<Book> ToModel(this IEnumerable<BookViewModel> bookViewModels)
        => bookViewModels.Select(item => item.ToModel());
}