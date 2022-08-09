namespace LibraryService.Web.Models;

public class Book
{
    public string? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Lang { get; set; } = string.Empty;

    public int Pages { get; set; }

    public int AgeLimit { get; set; }

    public Author[] Authors { get; set; } = default!;

    public string PublicationDate { get; set; } = string.Empty;
}