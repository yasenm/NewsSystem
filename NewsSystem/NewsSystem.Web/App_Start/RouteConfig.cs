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
                name: "NewsTags",
                url: "news/tag/{id}/{name}",
                defaults: new { controller = "News", action = "ByTagName" }
            );

            routes.MapRoute(
                name: "NewsCategories",
                url: "news/bycategory/{id}/{title}",
                defaults: new { controller = "News", action = "ByCategory" }
            );

            routes.MapRoute(
                name: "News",
                url: "news/{action}/{id}/{title}",
                defaults: new { controller = "News", action = "Details", title = UrlParameter.Optional }
            );

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
