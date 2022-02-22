using System.Runtime.InteropServices;
using WeatherForecastLibrary.Adapters;
using WeatherForecastLibrary.Interfaces;
using WeatherForecastLibrary.OutputStrategy;
using WeatherForecastLibrary.Pipeline;
using WeatherForecastLibrary.Pipeline.Items;

namespace WeatherForecastLibrary;

public class WeatherManagerBuilder
{
    internal readonly IHtmlProvider _provider;

    internal IWeatherForecastOutputStrategyPipeline? _outputStrategyPipeline;

    internal IHtmlParser? _parser;

    private WeatherManagerBuilder(IHtmlProvider provider) => _provider = provider;

    public static WeatherManagerBuilder Create(IHtmlProvider provider)
    {
        return new WeatherManagerBuilder(provider);
    }

    public IWeatherManager Build()
    {
        var result = new WeatherManager(_provider);

        var pipelineConfig = PipelineConfiguration<IWeatherOutputStrategyPipelineItem>.Create()
            .With(new ConsoleWeatherOutputStrategyPipelineItem());

        result.ConfigureOutputStrategy(_outputStrategyPipeline
            ?? WeatherForecastOutputStrategyPipeline.Create(pipelineConfig));

        result.ConfigureParser(_parser ?? new AgilityHtmlParser());

        return result;
    }

    public WeatherManagerBuilder WithParser(IHtmlParser parser)
    {
        _parser = parser;
        return this;
    }

    public WeatherManagerBuilder WithOutputStrategy(IWeatherForecastOutputStrategyPipeline outputStrategyPipeline)
    {
        _outputStrategyPipeline = outputStrategyPipeline;
        return this;
    }
}
