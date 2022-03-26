using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Interfaces;

namespace Restaurant.Booking;

public class Application : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IOrderManager _requestManager;

    public Application(ILogger<Application> logger, IOrderManager requestManager)
    {
        _logger = logger;
        _requestManager = requestManager;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("Application started");
            while (true)
            {
                _requestManager.ProcessRequest();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }

        return Task.CompletedTask;
    }
}