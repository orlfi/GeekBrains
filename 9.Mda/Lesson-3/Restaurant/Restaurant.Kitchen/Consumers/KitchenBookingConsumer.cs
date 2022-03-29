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
        var success = context.Message.Success;
        var orderId = context.Message.OrderId;

        if (success && context.Message.Dish is not null)
        {
            var kitchenIsReady = await _kitchen.CheckKitchenReadyAsync(context.Message.OrderId, context.Message.Dish);
            if (!kitchenIsReady)
            {
                // var kitchenRejectTask = context.Publish(new KitchenReject() { OrderId = orderId });
                // var kitchenReadyTask = context.Publish(new KitchenReady() { OrderId = orderId, Success = false });
                // await Task.WhenAll(kitchenRejectTask, kitchenReadyTask);
                await context.Publish(new KitchenReady() { OrderId = orderId, Success = false });
                _logger.LogInformation("Отмена кухни, блюдо в стоп листе: OrderId = {OrderId}", orderId);
            }
            else
            {
                await context.Publish(new KitchenReady() { OrderId = orderId, Success = true });
                _logger.LogInformation("Подтверждение кухни: OrderId = {OrderId}", orderId);
            }
        }
    }
}
