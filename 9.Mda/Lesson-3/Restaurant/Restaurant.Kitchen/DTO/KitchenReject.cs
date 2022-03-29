using Restaurant.Messaging.Data;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Booking.DTO;

public class KitchenReject : IKitchenReject
{
    public Guid OrderId { get; init; }
}
