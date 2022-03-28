using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Messaging.Interfaces;
using Restaurant.Notifications.Enums;
using Restaurant.Notifications.Interfaces;

namespace Restaurant.Notifications.Consumers;

public class KitchenConsumer : IConsumer<IKitchenReady>
{
    private readonly ILogger<KitchenConsumer> _logger;
    private readonly INotifier _notifier;

    public KitchenConsumer(ILogger<KitchenConsumer> logger, INotifier notifier)
    {
        _logger = logger;
        _notifier = notifier;
    }

    public Task Consume(ConsumeContext<IKitchenReady> context)
    {
        _logger.LogInformation("Получение сообщения KitchenReady от кухни для заказа: OrderId = {OrderId}", context.Message.OrderId);

        IKitchenReady kitchenReady = context.Message;
        _notifier.Accept(kitchenReady.OrderId, kitchenReady.Success ? Accepted.Kitchen : Accepted.Rejected);

        return Task.CompletedTask;
    }
}
