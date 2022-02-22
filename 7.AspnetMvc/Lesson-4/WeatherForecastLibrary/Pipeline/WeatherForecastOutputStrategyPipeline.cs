using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.Interfaces;
using WeatherForecastLibrary.Pipeline.Base;

namespace WeatherForecastLibrary.Pipeline
{

    public sealed class WeatherForecastOutputStrategyPipeline : IWeatherForecastOutputStrategyPipeline
    {
        private readonly IWeatherOutputStrategyPipelineItem _firstItem;

        public static WeatherForecastOutputStrategyPipeline Create(PipelineConfiguration<IWeatherOutputStrategyPipelineItem> configuration)
            => new WeatherForecastOutputStrategyPipeline(configuration);

        private WeatherForecastOutputStrategyPipeline(PipelineConfiguration<IWeatherOutputStrategyPipelineItem> configuration)
        {
            _firstItem = configuration.Build();
        }

        public void Run(IEnumerable<WeatherData> data)
        {
            if (_firstItem is null)
                throw new NullReferenceException("Pipeline не содержит элементов");

            _firstItem.Execute(data);
        }
    }
}