using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.OutputStrategy;
using WeatherForecastLibrary.Pipeline.Base;

namespace WeatherForecastLibrary.Pipeline.Items
{
    public sealed class ConsoleWeatherOutputStrategyPipelineItem : WeatherOutputStrategyPipelineItem
    {
        public ConsoleWeatherOutputStrategyPipelineItem()
        {
            _outputStrategy = new ConsoleWeatherOutputStrategy();
        }

        protected override bool Worker(IEnumerable<WeatherData> data)
        {
            if (data is null || _outputStrategy is null)
                return false;

            _outputStrategy.Process(data);
            return true;
        }
    }
}