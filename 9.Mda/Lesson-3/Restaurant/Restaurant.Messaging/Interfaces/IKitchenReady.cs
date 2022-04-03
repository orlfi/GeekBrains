using Restaurant.Messaging.Data;

namespace Restaurant.Messaging.Interfaces;

public interface IKitchenReady
{
    Guid OrderId { get; }
}
