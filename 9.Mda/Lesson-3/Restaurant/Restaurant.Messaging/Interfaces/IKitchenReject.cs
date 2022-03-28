using Restaurant.Messaging.Data;

namespace Restaurant.Messaging.Interfaces;

public interface IKitchenReject
{
    Guid OrderId { get; }
}
