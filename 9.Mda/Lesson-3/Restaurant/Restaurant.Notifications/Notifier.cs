using Restaurant.Notifications.Enums;
using Restaurant.Notifications.Interfaces;
using System.Collections.Concurrent;

namespace Restaurant.Notifications;

public class Notifier : INotifier
{
    private readonly ConcurrentDictionary<Guid, Tuple<Guid?, Accepted>> _state = new();

    public void Accept(Guid orderId, Accepted accepted, Guid? clientId = null)
    {
        _state.AddOrUpdate(orderId, new Tuple<Guid?, Accepted>(clientId, accepted),
            (id, oldValue) => new Tuple<Guid?, Accepted>(oldValue.Item1 ?? clientId, oldValue.Item2 | accepted));

        Notify(orderId);
    }

    private void Notify(Guid orderId)
    {
        var booking = _state[orderId];

        switch (booking.Item2)
        {
            case Accepted.All:
                Console.WriteLine("Успешно забронировано для клиента {0}", booking.Item1);
                _state.Remove(orderId, out _);
                break;
            case Accepted.Rejected:
                Console.WriteLine("Гость {0}, к сожалению все столики заняты", booking.Item1);
                _state.Remove(orderId, out _);
                break;
            case Accepted.Booking:
            case Accepted.Kitchen:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(booking.Item2));
        }
    }
}
