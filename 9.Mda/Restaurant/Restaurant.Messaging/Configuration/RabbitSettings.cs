namespace Restaurant.Messaging.Configuration;

public class RabbitSettings
{
    public string Host { get;  set;}
    public int Port { get; set; }
    public string ExchangeName { get; set; }
    public string ExchangeType { get; set; }
    public string QueueName { get; set; }
    public string RoutingKey { get; set; }
}
