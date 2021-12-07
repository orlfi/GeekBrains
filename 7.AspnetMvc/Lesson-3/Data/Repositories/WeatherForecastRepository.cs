using Data.Interfaces;
using Data.Models;

namespace Data.Repositories;
public class WeatherForecastRepository : IWeatherForecastRepository
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public IEnumerable<WeatherForecast> GetAll()
    {
        return Enumerable.Range(1, 10).Select(index => GetByDay(index)).ToArray();
    }

    public WeatherForecast GetByDay(int day)
    {
        return new WeatherForecast
        (
            DateTime.Now.AddDays(day),
            Random.Shared.Next(-20, 55),
            Summaries[Random.Shared.Next(Summaries.Length)]
        );
    }
}

