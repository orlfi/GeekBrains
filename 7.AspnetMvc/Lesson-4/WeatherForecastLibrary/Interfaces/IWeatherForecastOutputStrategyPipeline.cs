using WeatherForecastLibrary.Data;

namespace WeatherForecastLibrary.Interfaces;
public interface IWeatherForecastOutputStrategyPipeline
{
    void Run(IEnumerable<WeatherData> data);
}
