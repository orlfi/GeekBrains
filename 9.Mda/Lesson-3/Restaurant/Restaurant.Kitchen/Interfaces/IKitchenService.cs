using Restaurant.Messaging.Data;

namespace Restaurant.Kitchen.Interfaces;

internal interface IKitchenService
{
    Task<bool> CheckKitchenReadyAsync(Guid orderId, Dish dish);
}
