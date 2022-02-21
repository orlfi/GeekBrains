using System.Threading.Tasks;
using WeatherForecastLibrary.Interfaces;

namespace WeatherForecastLibrary.Pipeline
{
    public sealed class PipelineConfiguration<T> where T : IWeatherOutputStrategyPipelineItem
    {
        private T? _first;

        private T? _last;

        private PipelineConfiguration() { }

        public static PipelineConfiguration<T> Create()
        {
            return new PipelineConfiguration<T>();
        }

        public PipelineConfiguration<T> With(T item)
        {
            if (_first is null)
            {
                _first = item;
                _last = _first;
            }
            else
            {
                if (_last != null)
                    _last.Next = item;

                _last = item;
            }
            return this;
        }

        public T Build()
        {
            if (_first is null)
                throw new NullReferenceException("Pipeline не может быть пустой");

            return _first;
        }
    }
}