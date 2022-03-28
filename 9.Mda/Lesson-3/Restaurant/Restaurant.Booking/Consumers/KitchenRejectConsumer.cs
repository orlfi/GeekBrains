using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.Interfaces;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Notifications.Consumers;

internal class KitchenRejectConsumer : IConsumer<IKitchenReject>
{
    private readonly ILogger<KitchenRejectConsumer> _logger;
    private readonly ITableBookingService _tableBookingService;

    public KitchenRejectConsumer(ILogger<KitchenRejectConsumer> logger, ITableBookingService tableBookingService)
    {
        _logger = logger;
        _tableBookingService = tableBookingService;
    }

    public async Task Consume(ConsumeContext<IKitchenReject> context)
    {
        _logger.LogInformation("Получено сообщение отмены кухни: OrderId = {OrderId}", context.Message.OrderId);
        await _tableBookingService.ClearOrder(context.Message.OrderId);
    }
}
