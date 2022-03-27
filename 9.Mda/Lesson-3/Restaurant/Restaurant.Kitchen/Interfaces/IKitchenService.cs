using Restaurant.Messaging.Data;

namespace Restaurant.Kitchen.Interfaces;

internal interface IKitchenService
{
    Task CheckKitchenReadyAsync(Guid oerderId, Dish dish);
}
