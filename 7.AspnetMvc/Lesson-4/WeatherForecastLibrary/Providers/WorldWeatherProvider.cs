using WeatherForecastLibrary.Interfaces;

namespace Providers;

public class WorldWeatherProvider : IHtmlProvider
{
    private readonly string _ulr = "https://world-weather.ru/pogoda/russia/novomoskovsk/";

    private HttpClient _client;

    public WorldWeatherProvider(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetPageAsync()
    {
        using var response = await _client.GetAsync(_ulr);
        return await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
    }
}
