namespace WeatherForecastLibrary.Interfaces;

public interface IHtmlProvider
{
    Task<string> GetPageAsync();
}
