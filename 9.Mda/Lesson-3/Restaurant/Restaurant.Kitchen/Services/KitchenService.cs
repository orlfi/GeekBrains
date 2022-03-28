using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Kitchen.Interfaces;
using Restaurant.Messaging.Data;

namespace Restaurant.Kitchen.Services;

internal class KitchenService : IKitchenService
{
    private List<int> _stopList;
    private const int checkTime = 300;

    private readonly ILogger<KitchenService> _logger;
    private readonly IBus _bus;

    public KitchenService(ILogger<KitchenService> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
        _stopList = new List<int> { 2 };
    }

    public async Task CheckKitchenReadyAsync(Guid orderId, Dish dish)
    {
        await Task.Delay(checkTime);
        if (_stopList.Contains(dish.Id))
        {
            await _bus.Publish(new KitchenReject() { OrderId = orderId });
            _logger.LogInformation("Отмена кухни. Блюдо в стоп листе: OrderId = {OrderId}", orderId);
        }
        else
        {
            await _bus.Publish(new KitchenReady() { OrderId = orderId, Success = true });
            _logger.LogInformation("Подтверждение кухни: OrderId = {OrderId}", orderId);
        }
    }
}
