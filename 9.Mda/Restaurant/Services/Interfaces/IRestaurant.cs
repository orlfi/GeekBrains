namespace Services.Interfaces;

public interface IRestaurant
{
    void BookFreeTable();
    Task BookFreeTableAsync(CancellationToken cancel = default);
    void ClearBook(int number);
    Task ClearBookAsync(int number, CancellationToken cancel = default);
    void Dispose();
}
