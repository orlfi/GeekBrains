using System.Text;
using Restaurant.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Restaurant.Messaging.Configuration;

namespace Restaurant.Messaging.Mq;

public class RabbitProducer : IProducer, IDisposable
{
    private bool _disposed;
    private RabbitSettings _settings;
    private readonly ILogger<RabbitProducer> _logger;
    private IConnection _connection;
    private IModel _channel;
    
    public string Name => "MQ";

    public RabbitProducer(RabbitSettings settings, ILogger<RabbitProducer> logger)
    {
        _settings = settings;
        _logger = logger;
        InitilizeConnection();
    }

    public void Send(string message)
    {
        if (_connection is null)
            throw new NullReferenceException("Отсутствует соединение");

        if (_channel is null)
            throw new NullReferenceException("Отсутствует канал");

        if (string.IsNullOrEmpty(message))
        {
            _logger.LogWarning("Пустое сообщение отправлено не будет");
            return;
        }

        _channel.ExchangeDeclare(exchange:_settings.ExchangeName, type:_settings.ExchangeType, durable: _settings.Durable, false, null);

        var data = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange:_settings.ExchangeName, routingKey:_settings.RoutingKey, null, data);
        _logger.LogInformation("Отправлено сообщение {0}", message);
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
