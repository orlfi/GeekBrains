using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Notifications;

public class Background : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IConsumer _consumer;

    public Background(ILogger<Background> logger, IConsumer consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("Application started");
            _consumer.Recieve((sender, args)=>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Получено: {0} in {1}", message, Thread.CurrentThread.ManagedThreadId);
                //Thread.Sleep(5000);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }

        return Task.CompletedTask;
    }
}