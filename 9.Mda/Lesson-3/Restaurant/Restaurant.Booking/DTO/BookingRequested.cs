﻿using Restaurant.Messaging.Data;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Booking.DTO;

public class BookingRequested : IBookingRequested
{
    public Guid OrderId { get; init; }
    public Guid ClientId { get; init; }
    public Dish? Dish { get; init; }
    public int Seats { get; init; }
    public int BookingArrivalTime { get; init; }
    public int ActualArrivalTime { get; init; }
}
