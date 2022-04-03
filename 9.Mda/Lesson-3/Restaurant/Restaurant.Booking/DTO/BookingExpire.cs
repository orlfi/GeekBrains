using Restaurant.Messaging.Data;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Booking.DTO;

public class BookingExpire : IBookingExpired
{
    private readonly RestaurantState _state;

    public Guid OrderId => _state.OrderId;

    public BookingExpire(RestaurantState state)
    {
        _state = state;
    }
}
