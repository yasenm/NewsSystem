namespace NewsSystem.Web
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Web.App_Start;
    using NewsSystem.Data.Services.Articles;
    using NewsSystem.Data.ViewModels.Articles;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEnginesConfiguration.RegisterViewEngines(ViewEngines.Engines);

            // Automapper config
            var autoMapperConfig = new AutoMapperConfig(Assembly.GetAssembly(typeof(ArticleViewModel)));
            autoMapperConfig.Execute();

            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
