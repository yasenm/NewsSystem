namespace NewsSystem.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NewsSystem.Web.Controllers" }
            );

            routes.MapRoute(
                name: "DefaultHome",
                url: "AdminPanel",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "NewsSystem.Web.Areas.AdminPanel.Controllers" }
            );

            routes.MapRoute(
                name: "DefaultAdmin",
                url: "AdminPanel/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NewsSystem.Web.Areas.AdminPanel.Controllers" }
            );
        }
    }
}
