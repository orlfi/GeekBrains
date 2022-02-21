using System.Security.Cryptography.X509Certificates;
using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.Interfaces;

namespace WeatherForecastLibrary.Pipeline.Base
{

    public abstract class WeatherOutputStrategyPipelineItem : IWeatherOutputStrategyPipelineItem
    {
        protected IWeatherOutputStrategy? _outputStrategy;

        public IWeatherOutputStrategyPipelineItem? Next { get; set; }

        public bool Execute(IEnumerable<WeatherData> data)
        {
            if (!Worker(data) || Next is null)
            {
                return false;
            }

            Next.Execute(data);
            return true;
        }

        protected abstract bool Worker(IEnumerable<WeatherData> data);
    }
}