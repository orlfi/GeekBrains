namespace Services.Interfaces;

public interface IRestaurant
{
    void BookFreeTable(int seatsCount);
    void BookFreeTableAsync(int seatsCount, CancellationToken cancel = default);
    void ClearBook(int number);
    void ClearBookAsync(int number, CancellationToken cancel = default);
    void Dispose();
}
