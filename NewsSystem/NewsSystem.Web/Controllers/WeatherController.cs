using NewsSystem.Web.Models.Weather;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;

namespace NewsSystem.Web.Controllers
{
    public class WeatherController : Controller
    {
        [OutputCache(Duration = 3000)]
        public ActionResult City()
        {
            try
            {
                var webClient = new WebClient();
                var weatherAPIKey = WebConfigurationManager.AppSettings["weatherAPIKey"];
                var weatherCitiesIds = WebConfigurationManager.AppSettings["weatherCitiesIds"];
                //test result
                var result = "{\"cnt\":3,\"list\":[{\"coord\":{\"lon\":37.62,\"lat\":55.75},\"sys\":{\"type\":1,\"id\":7323,\"message\":0.1872,\"country\":\"RU\",\"sunrise\":1481262451,\"sunset\":1481288219},\"weather\":[{\"id\":701,\"main\":\"Mist\",\"description\":\"mist\",\"icon\":\"50n\"},{\"id\":600,\"main\":\"Snow\",\"description\":\"light snow\",\"icon\":\"13n\"}],\"main\":{\"temp\":-3.74,\"pressure\":996,\"humidity\":92,\"temp_min\":-5,\"temp_max\":-3},\"visibility\":5000,\"wind\":{\"speed\":4,\"deg\":210},\"clouds\":{\"all\":90},\"dt\":1481290200,\"id\":524901,\"name\":\"Moscow\"},{\"coord\":{\"lon\":30.52,\"lat\":50.43},\"sys\":{\"type\":1,\"id\":7358,\"message\":0.1811,\"country\":\"UA\",\"sunrise\":1481262422,\"sunset\":1481291655},\"weather\":[{\"id\":300,\"main\":\"Drizzle\",\"description\":\"light intensity drizzle\",\"icon\":\"09d\"},{\"id\":701,\"main\":\"Mist\",\"description\":\"mist\",\"icon\":\"50d\"},{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"main\":{\"temp\":5.34,\"pressure\":1009,\"humidity\":100,\"temp_min\":5,\"temp_max\":6},\"visibility\":3300,\"wind\":{\"speed\":7,\"deg\":280},\"clouds\":{\"all\":90},\"dt\":1481290200,\"id\":703448,\"name\":\"Kiev\"},{\"coord\":{\"lon\":-0.13,\"lat\":51.51},\"sys\":{\"type\":1,\"id\":5091,\"message\":0.177,\"country\":\"GB\",\"sunrise\":1481270090,\"sunset\":1481298701},\"weather\":[{\"id\":721,\"main\":\"Haze\",\"description\":\"haze\",\"icon\":\"50d\"}],\"main\":{\"temp\":13.54,\"pressure\":1025,\"humidity\":82,\"temp_min\":13,\"temp_max\":14},\"visibility\":10000,\"wind\":{\"speed\":5.1,\"deg\":230},\"clouds\":{\"all\":0},\"dt\":1481289600,\"id\":2643743,\"name\":\"London\"}]}";
                //var result = webClient.DownloadString(new Uri($"http://api.openweathermap.org/data/2.5/group?id{weatherCitiesIds}=&appid={weatherAPIKey}&lang=bg&units=metric"));
                var model = JsonConvert.DeserializeObject<WeatherCollectionViewModel>(result);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400);
            }
        }
    }
}