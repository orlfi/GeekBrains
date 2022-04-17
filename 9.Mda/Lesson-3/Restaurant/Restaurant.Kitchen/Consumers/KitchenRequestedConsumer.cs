using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Kitchen.Interfaces;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Notifications.Consumers;

internal class KitchenRequestedConsumer : IConsumer<ITableBooked>
{
    private readonly ILogger<KitchenRequestedConsumer> _logger;
    private readonly IKitchenService _kitchen;

    public KitchenRequestedConsumer(ILogger<KitchenRequestedConsumer> logger, IKitchenService kitchen)
    {
        _logger = logger;
        _kitchen = kitchen;
    }

    public async Task Consume(ConsumeContext<ITableBooked> context)
    {
        _logger.LogInformation("Получение сообщения TableBooked от сервиса бронирования: OrderId = {OrderId}", context.Message.OrderId);
        var orderId = context.Message.OrderId;

        if (context.Message.Dish is null)
        {
            _logger.LogInformation("Поле <Блюдо> должно быть заполнено: OrderId = {OrderId}", orderId);
            throw new NullReferenceException("Поле <Блюдо> должно быть заполнено");
        }

        var kitchenIsReady = await _kitchen.CheckKitchenReadyAsync(context.Message.OrderId, context.Message.Dish);
        if (kitchenIsReady)
        {
            await context.Publish(new KitchenReady() { OrderId = orderId });
            _logger.LogInformation("Подтверждение кухни: OrderId = {OrderId}", orderId);
        }
        else
        {
            await context.Publish(new KitchenReject() { OrderId = orderId });
            _logger.LogInformation("Отмена кухни, блюдо в стоп листе: OrderId = {OrderId}", orderId);
        }
    }
}
