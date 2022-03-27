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
        var success = context.Message.Success;

        if (success)
        {
            await _kitchen.CheckKitchenReadyAsync(context.Message.OrderId, context.Message.Dish);
            _logger.LogInformation("Recieved message: OrderId = {OrderId}", context.Message.OrderId);
        }
    }
}
