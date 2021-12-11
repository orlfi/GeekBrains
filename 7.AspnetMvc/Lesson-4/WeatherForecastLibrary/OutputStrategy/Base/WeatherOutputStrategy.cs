using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.Interfaces;

namespace WeatherForecastLibrary.OutputStrategy.Base;

internal abstract class WeatherOutputStrategy : IWeatherOutputStrategy
{
    public abstract void Process(IEnumerable<WeatherData> data);
}
