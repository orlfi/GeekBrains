using RabbitMQ.Client.Events;

namespace Restaurant.Messaging.Interfaces;

public interface IConsumer
{
    void Recieve(EventHandler<BasicDeliverEventArgs> recieveCallback);
}
