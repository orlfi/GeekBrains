using SampleService.DTO;

namespace SampleService.Services;

public interface IRootServiceClient
{
    Task<ICollection<WeatherForecast>> GetAsync();
}