using Restaurant.Messaging.Data;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Booking.Models;

internal class KitchenRequestModel
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public Dish? Dish { get; private set; }

    public KitchenRequestModel(ITableBooked tableBooked)
        : this(tableBooked.OrderId, tableBooked.ClientId, tableBooked.Dish) { }

    public KitchenRequestModel(Guid orderId, Guid clientId, Dish? dish)
    {
        OrderId = orderId;
        ClientId = clientId;
        Dish = dish;
    }

    // public KitchenRequestModel Update(ITableBooked tableBooked, string id)
    // {
    //     OrderId = tableBooked.OrderId;
    //     ClientId = tableBooked.ClientId;
    //     Dish = tableBooked.Dish;

    //     return this;
    // }

    // public bool Contains(string messageId)
    // {
    //     return _messageIds.Contains(messageId);
    // }
}
