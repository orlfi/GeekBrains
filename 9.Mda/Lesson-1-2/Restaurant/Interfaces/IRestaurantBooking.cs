﻿namespace Interfaces;

public interface IRestaurantBooking
{
    void BookFreeTable(int seatsCount);
    void BookFreeTableAsync(int seatsCount, CancellationToken cancel = default);
    void RemoveBookingByNumber(int number);
    void RemoveBookingByNumberAsync(int number, CancellationToken cancel = default);
    void Dispose();
}
