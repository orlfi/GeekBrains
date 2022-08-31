using SampleService.DTO;

namespace SampleService.Services;

public class RootServiceClient : IRootServiceClient
{
    private readonly HttpClient _httpClient;

    public RootServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ICollection<WeatherForecast>> GetAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<WeatherForecast>>("http://localhost:5125/WeatherForecast");
    }
}