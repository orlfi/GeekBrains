using WeatherForecastLibrary.Adapters;
using WeatherForecastLibrary.Interfaces;
using WeatherForecastLibrary.OutputStrategy;

namespace WeatherForecastLibrary;

public class WeatherManager : IWeatherManager
{
    private readonly IHtmlProvider? _provider;

    private IWeatherForecastOutputStrategyPipeline? _outputStrategyPipeline;

    private IHtmlParser? _parser;

    private WeatherManager() { }

    internal WeatherManager(IHtmlProvider provider) => _provider = provider;

    public void ConfigureParser(IHtmlParser parser) => _parser = parser;

    public void ConfigureOutputStrategy(IWeatherForecastOutputStrategyPipeline outputStrategyPipeline) => _outputStrategyPipeline = outputStrategyPipeline;

    public async Task Run()
    {
        if (_provider is null)
            throw new NullReferenceException("HTML провайдер не задан");

        if (_parser is null)
            throw new NullReferenceException("Парсер не задан");

        if (_outputStrategyPipeline is null)
            throw new NullReferenceException("Выходная цепочка не задана");

        var html = await _provider.GetPageAsync("https://world-weather.ru/pogoda/russia/novomoskovsk/");

        var data = _parser.GetWeatherForecast(html);

        _outputStrategyPipeline.Run(data);
    }
}
