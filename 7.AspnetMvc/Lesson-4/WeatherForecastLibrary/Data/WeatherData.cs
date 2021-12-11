using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastLibrary.Data;

public class WeatherData
{
    public DateTime Date { get; set; }

    public int DayTemperature { get; set; }

    public int NigthTemperature { get; set; }

    public string? Description { get; set; }
}
