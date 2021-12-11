using Providers;
using WeatherForecastLibrary;
using WeatherForecastLibrary.Adapters;
using WeatherForecastLibrary.Interfaces;
using WeatherForecastLibrary.Pipeline;
using WeatherForecastLibrary.Pipeline.Items;

namespace app;

static class Program
{
    static HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    {
        var manager = WeatherManagerBuilder.Create(new HtmlProvider(client))
            .WithOutputStrategy(WeatherForecastOutputStrategyPipeline
            .Create(PipelineConfiguration<IWeatherOutputStrategyPipelineItem>.Create()
                .With(new ConsoleWeatherOutputStrategyPipelineItem())
                .With(new FileWeatherOutputStrategyPipelineItem("WeatherForecast.txt"))))
            .WithParser(new AgilityHtmlParser())
            .Build();

        try
        {
            await manager.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }
}
