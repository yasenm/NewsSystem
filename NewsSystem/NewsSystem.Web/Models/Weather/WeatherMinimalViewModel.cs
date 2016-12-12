using Newtonsoft.Json;
using System.Collections.Generic;

namespace NewsSystem.Web.Models.Weather
{
    [JsonObject]
    public class WeatherMinimalViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cod { get; set; }
        public string Base { get; set; }
        public string Visibility { get; set; }
        public WeatherAPICoord Coord { get; set; }
        public List<WeatherAPIWeather> Weather { get; set; }
        public WeatherAPIMain Main { get; set; }
        public WeatherAPIWind Wind { get; set; }
        public WeatherAPISys Sys { get; set; }
        public WeatherAPIClouds Clouds { get; set; }
        //"dt": 1481194800,
    }
}