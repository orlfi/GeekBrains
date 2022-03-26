using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Restaurant.Messaging.Configuration;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Messaging.Mq;

public class RabbitConsumer : IConsumer, IDisposable
{
    private bool _disposed;
    private RabbitSettings _settings;
    private readonly ILogger<RabbitProducer> _logger;
    private IConnection _connection;
    private IModel _channel;
    
    public string Name => "MQ";

    public RabbitConsumer(RabbitSettings settings, ILogger<RabbitProducer> logger)
    {
        _settings = settings;
        _logger = logger;
        InitilizeConnection();
    }

    public void Recieve(EventHandler<BasicDeliverEventArgs> recieveCallback)
    {
        if (_connection is null)
            throw new NullReferenceException("Отсутствует соединение");

        if (_channel is null)
            throw new NullReferenceException("Отсутствует канал");


        _channel.ExchangeDeclare(_settings.ExchangeName, _settings.ExchangeType);

        string queueName = _settings.QueueName;
        if (string.IsNullOrEmpty(queueName))
            queueName = _channel.QueueDeclare().QueueName;
        else
            _channel.QueueDeclare(_settings.QueueName, false, false, false, null);

        
        _channel.QueueBind(queueName, _settings.ExchangeName, _settings.RoutingKey);
        
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += recieveCallback;
        _channel.BasicConsume(queueName, _settings.Autoack, consumer);
    }

    public void Dispose()
    {
        if (_disposed)
            return;
        
        _channel?.Dispose();
        _connection?.Dispose();

        _disposed = true;
    }

    private void InitilizeConnection()
    {
        ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = _settings.Host, Port = _settings.Port };
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }
}
