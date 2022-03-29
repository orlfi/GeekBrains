using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.Interfaces;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Notifications.Consumers;

internal class BookingKitchenReadyConsumer : IConsumer<IKitchenReady>
{
    private readonly ILogger<BookingKitchenReadyConsumer> _logger;
    private readonly ITableBookingService _tableBookingService;

    public BookingKitchenReadyConsumer(ILogger<BookingKitchenReadyConsumer> logger, ITableBookingService tableBookingService)
    {
        _logger = logger;
        _tableBookingService = tableBookingService;
    }

    public async Task Consume(ConsumeContext<IKitchenReady> context)
    {
        var success = context.Message.Success;
        if (!success)
        {
            _logger.LogInformation("Получено сообщение об отмене кухни [KitchenReady = {Success}]: OrderId = {OrderId}", success, context.Message.OrderId);
            await _tableBookingService.ClearOrder(context.Message.OrderId);
        }
    }
}
