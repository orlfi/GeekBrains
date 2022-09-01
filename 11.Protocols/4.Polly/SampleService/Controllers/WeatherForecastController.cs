using Microsoft.AspNetCore.Mvc;
using SampleService.DTO;
using SampleService.Services;

namespace SampleService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IRootServiceClient _rootClient;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IRootServiceClient rootClient)
    {
        _logger = logger;
        _rootClient = rootClient;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        _logger.LogInformation("Start root service request...");
        var result = await _rootClient.GetAsync();
        _logger.LogInformation("End root service request...");
        return result;
    }
}
