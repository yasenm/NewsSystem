namespace NewsSystem.Web.Controllers.Base
{
    using System.Web.Mvc;
    using System.Threading;

    using Microsoft.AspNet.Identity;
    using System.Net;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Web.Configuration;
    using Models.Captcha;
    using System.Web;
    using NewsSystem.Common.Helpers;
    using System;

    public class BaseController : Controller
    {
        private string _cryptKey
        {
            get
            {
                return WebConfigurationManager.AppSettings["cookiesCryptKey"];
            }
        }

        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }

        public void SetCookie(string key, string value)
        {
            value = EncryptionHelper.Encrypt(_cryptKey, value);
            var cookie = GetCookie(key);
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddYears(10);
            Response.Cookies.Add(cookie);
        }

        //public HttpCookie SetCookie(string key)
        //{
        //    HttpCookie cookie;
        //    if (Request.Cookies[key] == null)
        //    {
        //        cookie = new HttpCookie(key);
        //    }
        //    else
        //    {
        //        cookie = (HttpCookie)Request.Cookies[key];
        //    }
        //    return cookie;
        //}

        public HttpCookie GetCookie(string key)
        {
            if (Request.Cookies[key] == null)
            {
                return null;
            }
            return (HttpCookie)Request.Cookies[key]; ;
        }

        public string GetCookieVal(string key)
        {
            if (Request.Cookies[key] == null)
            {
                return string.Empty;
            }
            return EncryptionHelper.Decrypt(_cryptKey, ((HttpCookie)Request.Cookies[key]).Value);
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
}