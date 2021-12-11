using WeatherForecastLibrary.Interfaces;

namespace Providers;

public class HtmlProvider : IHtmlProvider
{
    private HttpClient _client;

    public HtmlProvider(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetPageAsync(string path)
    {
        using var response = await _client.GetAsync(path);
        return await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
    }
}
