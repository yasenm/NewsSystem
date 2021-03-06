﻿namespace NewsSystem.Web.Helpers
{
    using NewsSystem.Common;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public static class UrlHelpers
    {
        /// <summary>
        /// Generates a fully qualified URL to an action method by using
        /// the specified action name, controller name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteAction(this UrlHelper url,
            string actionName, string controllerName, object routeValues = null)
        {
            string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;

            return url.Action(actionName, controllerName, routeValues, scheme);
        }


        public static string CleanerUrl(this HtmlHelper htmlHelper, string title)
        {
            string cleanTitle = title.Trim().ToLower().Replace(" ", "-");
            //Removes invalid character like .,-_ etc
            //cleanTitle = Regex.Replace(cleanTitle, @"[^a-zA-Z0-9\/_|+ -]", "");
            cleanTitle = CyrilicStringTransliterator.TranslateToEnglishLetters(cleanTitle);
            return cleanTitle;
        }
    }
}
