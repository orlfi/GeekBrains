using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.OutputStrategy;
using WeatherForecastLibrary.Pipeline.Base;

namespace WeatherForecastLibrary.Pipeline.Items
{
    public sealed class FileWeatherOutputStrategyPipelineItem : WeatherOutputStrategyPipelineItem
    {
        public FileWeatherOutputStrategyPipelineItem(string fileName)
        {
            _outputStrategy = new FileWeatherOutputStrategy(fileName);
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