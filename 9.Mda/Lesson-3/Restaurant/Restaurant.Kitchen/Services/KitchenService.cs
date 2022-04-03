using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Kitchen.Interfaces;
using Restaurant.Messaging.Data;

namespace Restaurant.Kitchen.Services;

internal class KitchenService : IKitchenService
{
    private readonly List<int> _stopList;
    
    private readonly ILogger<KitchenService> _logger;

    public KitchenService(ILogger<KitchenService> logger)
    {
        _logger = logger;
        _stopList = new List<int> { 2 };
    }

    public async Task<bool> CheckKitchenReadyAsync(Guid orderId, Dish dish)
    {
        var checkTime = Random.Shared.Next(1, 5) * 1000;
        return false;

        _logger.LogInformation("Проверка блюда {DishId} {DishName} в стоп-листе за {CheckTime} сек", dish.Id, dish.Name, checkTime);
        await Task.Delay(checkTime);
        return !_stopList.Contains(dish.Id);
    }
}
