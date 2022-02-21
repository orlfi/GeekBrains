using WeatherForecastLibrary.Data;

namespace WeatherForecastLibrary.Interfaces;

public interface IHtmlParser
{
    IEnumerable<WeatherData> GetWeatherForecast(string html);
}
