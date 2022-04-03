using Restaurant.Messaging.Data;

namespace Restaurant.Messaging.Interfaces;

public interface IBookingExpired
{
    Guid OrderId { get; }
}
