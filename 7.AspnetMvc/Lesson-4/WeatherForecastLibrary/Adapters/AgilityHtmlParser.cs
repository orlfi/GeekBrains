using HtmlAgilityPack;
using WeatherForecastLibrary.Adapters.Base;
using WeatherForecastLibrary.Data;

namespace WeatherForecastLibrary.Adapters
{
    public sealed class AgilityHtmlParser : HtmlParser
    {
        public override IEnumerable<WeatherData> GetWeatherForecast(string html)
        {

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var nodes = htmlDoc.DocumentNode.SelectNodes(".//li[@class='tab-w']");
            return nodes.Select(node => GetDayWeatherForecast(node)).ToArray();
        }

        private WeatherData GetDayWeatherForecast(HtmlNode node)
        {
            var day = node.SelectSingleNode("div[@class='numbers-month']").InnerText;
            var month = node.SelectSingleNode("div[@class='month']").InnerText;
            var dayTemperatureString = node.SelectSingleNode("div[@class='day-temperature']").InnerText;
            var nigthTemperatureString = node.SelectSingleNode("div[@class='night-temperature']").InnerText;
            var description = node.SelectSingleNode("span[contains(concat(' ', @class, ' '), ' wi ')]").Attributes["title"].Value;

            if (!DateTime.TryParse(day + month, out DateTime date))
            {
                date = DateTime.Today;
            }

            date = date < DateTime.Today ? date.AddYears(1) : date;
            return new WeatherData
            {
                Date = date,
                DayTemperature = int.Parse(dayTemperatureString.Substring(0, dayTemperatureString.Length - 1)),
                NigthTemperature = int.Parse(nigthTemperatureString.Substring(0, nigthTemperatureString.Length - 1)),
                Description = description
            };

        }
    }
}