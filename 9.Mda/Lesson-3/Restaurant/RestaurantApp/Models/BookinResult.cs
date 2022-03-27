
namespace Restaurant.Booking.Models;

internal struct BookinResult
{
    public bool Success => string.IsNullOrEmpty(Error) && TableNumbers.Count > 0;

    public string Error { get; init; } = string.Empty;

    public ICollection<int> TableNumbers { get; init; }  = new List<int>();

    public BookinResult(List<int> tableNumbers) => TableNumbers = tableNumbers;

    public BookinResult(string error) => Error = error;

    public static implicit operator BookinResult(List<int> tableNumbers) => new BookinResult(tableNumbers);
    
    public static implicit operator BookinResult(Exception ex) => new BookinResult(ex.Message);

    public static implicit operator BookinResult(String error) => new BookinResult(error);

    public static implicit operator List<int>(BookinResult bookinResult) => (List<int>)bookinResult.TableNumbers;

    public static implicit operator string(BookinResult bookinResult) => bookinResult.Error;
}
