using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.Interfaces;

namespace WeatherForecastLibrary.Adapters.Base;

public abstract class HtmlParser : IHtmlParser
{
    public abstract IEnumerable<WeatherData> GetWeatherForecast(string html);
}
