using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.Pipeline.Base;

namespace WeatherForecastLibrary.Interfaces;

public interface IWeatherOutputStrategyPipelineItem
{
    IWeatherOutputStrategyPipelineItem? Next { get; set; }

    bool Execute(IEnumerable<WeatherData> data);
}
