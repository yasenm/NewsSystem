namespace NewsSystem.Web.Controllers.Base
{
    using System.Web.Mvc;
    using System.Threading;

    using Microsoft.AspNet.Identity;
    using System.Net;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Web.Configuration;

    public class BaseController : Controller
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }

        public bool ValidateRecaptcha()
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            string secret = WebConfigurationManager.AppSettings["reCaptchaSecretKey"];

            var client = new WebClient();
            var reply = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={response}");

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ViewBag.Message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        ViewBag.Message = "The secret parameter is invalid or malformed.";
                        break;

                    case ("missing-input-response"):
                        ViewBag.Message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        ViewBag.Message = "The response parameter is invalid or malformed.";
                        break;

                    default:
                        ViewBag.Message = "Error occured. Please try again";
                        break;
                }
                return false;
            }
            else
            {
                return true;
                ViewBag.Message = "Valid";
            }
        }
	}

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}