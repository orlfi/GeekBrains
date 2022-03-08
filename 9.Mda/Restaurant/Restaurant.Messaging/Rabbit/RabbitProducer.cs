using System.Text;
using Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Restaurant.Messaging.Configuration;

namespace Restaurant.Messaging.Mq;

public class RabbitProducer : IProducer, IDisposable
{
    private bool _disposed;
    private IConnection _connection;
    private string _queueName = "booking";
    private string _exchangeName;
    private string _exchangeType;
    private readonly ILogger<RabbitProducer> _logger;

    public string Name => "MQ";

    public RabbitProducer(RabbitSettings settings, ILogger<RabbitProducer> logger)
    {
        ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = settings.Host, Port = settings.Port};
        _exchangeName = settings.ExchangeName;
        _exchangeType = settings.ExchangeType;  
        _connection = connectionFactory.CreateConnection();
        _logger = logger;
    }


    public Task SendAsync(string message, CancellationToken cancel = default)
    {
        if (_connection is null)
            throw new NullReferenceException("Отсутствует соединение");

        if (string.IsNullOrEmpty(message))
        {
            _logger.LogWarning("Пустое сообщение отправлено не будет");
            return Task.CompletedTask;
        }

        using var channel = _connection.CreateModel();
        channel.ExchangeDeclare(_exchangeName, _exchangeType, false, false, null);

        var data = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(_exchangeName, _queueName, null, data);
        _logger.LogInformation("Отправлено сообщение {0}", message);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_disposed)
            return;
        
        _connection?.Dispose();

        _disposed = true;
    }
}
