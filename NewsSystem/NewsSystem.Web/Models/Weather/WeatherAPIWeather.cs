using System.Text;

namespace NewsSystem.Web.Models.Weather
{
    public class WeatherAPIWeather
    {
        public string Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string DecodedDescription
        {
            get
            {
                byte[] decoded = Encoding.GetEncoding("iso-8859-1").GetBytes(Description);
                string text = Encoding.UTF8.GetString(decoded);
                return text;
            }
        }
        public string Icon { get; set; }
    }
}