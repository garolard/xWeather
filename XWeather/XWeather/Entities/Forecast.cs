using System.Collections.Generic;

namespace XWeather.Entities
{
    public class Forecast
    {
        public City city { get; set; }
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<CurrentWeather> list { get; set; }
    }
}