using Restaurant.Notifications.Enums;

namespace Restaurant.Notifications.Interfaces;

public interface INotifier
{
    void Accept(Guid orderId, Accepted accepted, Guid? clientId = null);
}
