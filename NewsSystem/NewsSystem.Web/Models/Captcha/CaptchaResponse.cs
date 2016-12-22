using Newtonsoft.Json;
using System.Collections.Generic;

namespace NewsSystem.Web.Models.Captcha
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
