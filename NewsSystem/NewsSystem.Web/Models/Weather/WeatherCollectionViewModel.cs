using System.Collections.Generic;

namespace NewsSystem.Web.Models.Weather
{
    public class WeatherCollectionViewModel
    {
        public string Cnt { get; set; }
        public IList<WeatherMinimalViewModel> List { get; set; }
    }
}