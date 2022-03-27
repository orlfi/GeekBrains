using Restaurant.Booking.Models;

namespace Restaurant.Booking.Interfaces;

internal interface ITableBookingService
{
    Task<BookinResult> BookFreeTableAsync(int seatsCount, CancellationToken cancel = default);
    bool RemoveBookingByNumberAsync(int number, CancellationToken cancel = default);
    void Dispose();
}
