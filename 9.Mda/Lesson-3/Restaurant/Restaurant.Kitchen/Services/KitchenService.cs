using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Kitchen.Interfaces;
using Restaurant.Messaging.Data;

namespace Restaurant.Kitchen.Services;

internal class KitchenService : IKitchenService
{
    private readonly List<int> _stopList;
    private const int checkTime = 300;

    private readonly ILogger<KitchenService> _logger;

    public KitchenService(ILogger<KitchenService> logger)
    {
        _logger = logger;
        _stopList = new List<int> { 2 };
    }

    public async Task<bool> CheckKitchenReadyAsync(Guid orderId, Dish dish)
    {
        _logger.LogDebug("Проверка блюда {DishName} в стоп-листе", dish.Name);
        await Task.Delay(checkTime);
        return _stopList.Contains(dish.Id);
    }
}
