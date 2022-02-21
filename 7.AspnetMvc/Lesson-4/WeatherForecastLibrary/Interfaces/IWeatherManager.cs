using WeatherForecastLibrary.Interfaces;

namespace WeatherForecastLibrary;

public interface IWeatherManager
{
    void ConfigureOutputStrategy(IWeatherForecastOutputStrategyPipeline outputStrategyPipeline);
    void ConfigureParser(IHtmlParser parser);
    Task Run();
}
