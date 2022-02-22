using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.OutputStrategy.Base;

namespace WeatherForecastLibrary.OutputStrategy;

internal sealed class ConsoleWeatherOutputStrategy : WeatherOutputStrategy
{
    public override void Process(IEnumerable<WeatherData> weatherForecast)
    {

        Console.Clear();
        Console.WriteLine("Дата           Дневная температура    Ночная температура    Описание");
        foreach (var item in weatherForecast)
        {
            Console.Write(item.Date.ToString("dd MMMM").PadRight(13));
            Console.Write(item.DayTemperature.ToString().PadLeft(21));
            Console.Write(item.NigthTemperature.ToString().PadLeft(22) + "    ");
            Console.WriteLine(item.Description);
        }
    }
}
