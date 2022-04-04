using Restaurant.Messaging.Data;

namespace Restaurant.Messaging.Interfaces;

public interface IBookingRequested
{
    Guid OrderId { get; }
    Guid ClientId { get; }
    int ArrivalTime { get; }
    int Seats { get; }
    Dish? Dish { get; }
}
