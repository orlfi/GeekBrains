using WeatherForecastLibrary.Data;

namespace WeatherForecastLibrary.Interfaces;

public interface IWeatherOutputStrategy
{
    void Process(IEnumerable<WeatherData> data);
}
