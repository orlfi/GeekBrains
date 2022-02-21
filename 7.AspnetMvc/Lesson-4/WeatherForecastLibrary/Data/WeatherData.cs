using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastLibrary.Data;

public readonly struct WeatherData
{
    public DateTime Date { get; init; }

    public int DayTemperature { get; init; }

    public int NigthTemperature { get; init; }

    public string? Description { get; init; }
}
