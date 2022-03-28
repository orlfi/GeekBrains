using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Messaging.Interfaces;
using Restaurant.Notifications.Enums;
using Restaurant.Notifications.Interfaces;

namespace Restaurant.Notifications.Consumers;

public class NotificationBookingConsumer : IConsumer<ITableBooked>
{
    private readonly ILogger<NotificationBookingConsumer> _logger;
    private readonly INotifier _notifier;

    public NotificationBookingConsumer(ILogger<NotificationBookingConsumer> logger, INotifier notifier)
    {
        _logger = logger;
        _notifier = notifier;
    }

    public Task Consume(ConsumeContext<ITableBooked> context)
    {
        _logger.LogInformation("Получение сособщения TableBooked [{Success}] от сервиса бронирования для заказа {OrderId}", context.Message.Success, context.Message.OrderId);

        ITableBooked tableBooked = context.Message;
        _notifier.Accept(tableBooked.OrderId, tableBooked.Success ? Accepted.Booking : Accepted.Rejected, tableBooked.ClientId);

        return Task.CompletedTask;
    }
}
