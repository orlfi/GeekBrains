using LibraryService.Web.Models;

namespace LibraryService.Web.ViewModels
{
    public class BookCategoryViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; } = default!;

        public SearchType SearchType { get; set; }
        public string? SearchString { get; set; }
    }
}