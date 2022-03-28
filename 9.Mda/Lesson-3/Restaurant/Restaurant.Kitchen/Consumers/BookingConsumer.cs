using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Kitchen.Interfaces;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Notifications.Consumers;

internal class BookingConsumer : IConsumer<ITableBooked>
{
    private readonly ILogger<BookingConsumer> _logger;
    private readonly IKitchenService _kitchen;

    public BookingConsumer(ILogger<BookingConsumer> logger, IKitchenService kitchen)
    {
        _logger = logger;
        _kitchen = kitchen;
    }

    public async Task Consume(ConsumeContext<ITableBooked> context)
    {
        _logger.LogInformation("Получение сообщения TableBooked от сервиса бронирования: OrderId = {OrderId}", context.Message.OrderId);
        var success = context.Message.Success;

        if (success)
        {
            Thread.Sleep(2000);
            await _kitchen.CheckKitchenReadyAsync(context.Message.OrderId, context.Message.Dish);
            await Task.Delay(4000);
        }
    }
}
