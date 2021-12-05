using Data.Models;

namespace Data.Interfaces
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> GetAll();
        
        WeatherForecast GetByDay(int day);
    }
}
