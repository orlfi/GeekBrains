using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Kitchen.Interfaces;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Notifications.Consumers;

internal class KitchenBookingConsumer : IConsumer<ITableBooked>
{
    private readonly ILogger<KitchenBookingConsumer> _logger;
    private readonly IKitchenService _kitchen;

    public KitchenBookingConsumer(ILogger<KitchenBookingConsumer> logger, IKitchenService kitchen)
    {
        _logger = logger;
        _kitchen = kitchen;
    }

    public async Task Consume(ConsumeContext<ITableBooked> context)
    {
        _logger.LogInformation("Получение сообщения TableBooked от сервиса бронирования: OrderId = {OrderId}", context.Message.OrderId);
        var orderId = context.Message.OrderId;

        if (context.Message.Dish is not null)
        {
            var kitchenIsReady = await _kitchen.CheckKitchenReadyAsync(context.Message.OrderId, context.Message.Dish);
            if (kitchenIsReady)
            {
                await context.Publish(new KitchenReady() { OrderId = orderId});
                _logger.LogInformation("Подтверждение кухни: OrderId = {OrderId}", orderId);
            }
            else
            {
                await context.Publish(new KitchenReject() { OrderId = orderId});
                _logger.LogInformation("Отмена кухни, блюдо в стоп листе: OrderId = {OrderId}", orderId);
            }
        }
    }
}
