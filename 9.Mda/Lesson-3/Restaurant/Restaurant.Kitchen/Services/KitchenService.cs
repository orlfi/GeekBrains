using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Kitchen.Interfaces;
using Restaurant.Messaging.Data;

namespace Restaurant.Kitchen.Services;

internal class KitchenService: IKitchenService
{
    private const int checkTime = 20000;

    private readonly ILogger<KitchenService> _logger;
    private readonly IBus _bus;

    public KitchenService(ILogger<KitchenService> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task CheckKitchenReadyAsync(Guid orderId, Dish dish)
    {
        await Task.Delay(checkTime);
        await _bus.Publish(new KitchenReady() { OrderId = orderId, Success = true });
    }
}
