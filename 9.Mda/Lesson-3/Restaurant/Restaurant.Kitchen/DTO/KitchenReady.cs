using Restaurant.Messaging.Data;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Booking.DTO;

public class KitchenReady : IKitchenReady
{
    public Guid OrderId { get; init; }
    public bool Success { get; init; }
}
