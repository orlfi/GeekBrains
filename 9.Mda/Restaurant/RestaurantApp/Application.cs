using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace RestaurantApp;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly ILogger _logger;
    private readonly IOrderManager _requestManager;

    public Application(ILogger<Application> logger, IOrderManager requestManager)
    {
        _logger = logger;
        _requestManager = requestManager;
    }

    public async Task RunAsync()
    {
        try
        {
            _logger.LogInformation("Application started");
            while (true)
            {
                _requestManager.ProcessRequest();
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }
}