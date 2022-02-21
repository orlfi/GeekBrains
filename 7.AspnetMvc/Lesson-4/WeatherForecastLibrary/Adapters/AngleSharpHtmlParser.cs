using AngleSharp;
using AngleSharp.Dom;
using WeatherForecastLibrary.Adapters.Base;
using WeatherForecastLibrary.Data;

namespace WeatherForecastLibrary.Adapters
{
    public sealed class AngleSharpHtmlParser : HtmlParser
    {
        public override IEnumerable<WeatherData> GetWeatherForecast(string html)
        {
            var config = Configuration.Default;
            using var context = BrowsingContext.New(config);
            using var doc = context.OpenAsync(req => req.Content(html)).Result;

            var nodes = doc.QuerySelectorAll(".tab-w");
            return nodes.Select(node => GetDayWeatherForecast(node)).ToArray();
        }

        private WeatherData GetDayWeatherForecast(IElement node)
        {
            var day = node.QuerySelector(".numbers-month")?.TextContent;
            var month = node.QuerySelector(".month")?.TextContent;
            var dayTemperatureString = node.QuerySelector(".day-temperature")?.TextContent;
            var nigthTemperatureString = node.QuerySelector(".night-temperature")?.TextContent;
            var description = node.QuerySelector(".wi")?.GetAttribute("title");

            if (day is null || month is null || !DateTime.TryParse(day + month, out DateTime date))
            {
                date = DateTime.Today;
            }

            date = date < DateTime.Today ? date.AddYears(1) : date;
            return new WeatherData
            {
                Date = date,
                DayTemperature = int.Parse(dayTemperatureString is null ? "" : dayTemperatureString.Substring(0, dayTemperatureString.Length - 1)),
                NigthTemperature = int.Parse(nigthTemperatureString is null ? "" : nigthTemperatureString.Substring(0, nigthTemperatureString.Length - 1)),
                Description = description
            };

        }
    }
}