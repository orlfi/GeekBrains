using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Messaging.Interfaces;
using Restaurant.Notifications.Enums;
using Restaurant.Notifications.Interfaces;

namespace Restaurant.Notifications.Consumers;

public class BookingConsumer : IConsumer<ITableBooked>
{
    private readonly ILogger<BookingConsumer> _logger;
    private readonly INotifier _notifier;

    public BookingConsumer(ILogger<BookingConsumer> logger, INotifier notifier)
    {
        _logger = logger;
        _notifier = notifier;
    }

    public Task Consume(ConsumeContext<ITableBooked> context)
    {
        _logger.LogInformation("Получение сособщения TableBooked от сервиса бронирования для заказа {OrderId}", context.Message.OrderId);

        ITableBooked tableBooked = context.Message;
        _notifier.Accept(tableBooked.OrderId, tableBooked.Success ? Accepted.Booking : Accepted.Rejected, tableBooked.ClientId);

        return Task.CompletedTask;
    }
}
