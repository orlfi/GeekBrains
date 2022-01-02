using Microsoft.Extensions.Logging;

namespace MailService;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly ILogger _logger;

    public Application(ILogger<Application> logger)
    {
        _logger = logger;
    }

    public async Task RunAsync()
    {
        try
        {
            await Task.Run(() => _logger.LogInformation("Application started"));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }
}