namespace NewsSystem.Web.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Web.Controllers.Base;

    public class HomeController : BaseController
    {
        private IArticleService ArticleService;

        public HomeController(IArticleService articleService)
        {
            this.ArticleService = articleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}