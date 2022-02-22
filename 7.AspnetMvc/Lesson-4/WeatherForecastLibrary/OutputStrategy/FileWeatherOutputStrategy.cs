using WeatherForecastLibrary.Data;
using WeatherForecastLibrary.OutputStrategy.Base;

namespace WeatherForecastLibrary.OutputStrategy;

internal sealed class FileWeatherOutputStrategy : WeatherOutputStrategy
{
    private readonly string _fileName;

    public FileWeatherOutputStrategy(string fileName) => _fileName = fileName;

    public override void Process(IEnumerable<WeatherData> weatherForecast)
    {
        using var writer = File.CreateText(_fileName);

        writer.WriteLine("Дата           Дневная температура    Ночная температура    Описание");
        foreach (var item in weatherForecast)
        {
            writer.Write(item.Date.ToString("dd MMMM").PadRight(13));
            writer.Write(item.DayTemperature.ToString().PadLeft(21));
            writer.Write(item.NigthTemperature.ToString().PadLeft(22) + "    ");
            writer.WriteLine(item.Description);
        }
    }
}
