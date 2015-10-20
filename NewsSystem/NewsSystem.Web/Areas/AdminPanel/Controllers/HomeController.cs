namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts;

    public class HomeController : Controller
    {
        private IArticleService ArticleService;

        public HomeController(IArticleService articleService)
        {
            this.ArticleService = articleService;
        }

        //
        // GET: /AdminPanel/Home/
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Articles()
        {
            var articlesCollection = this.ArticleService.GetAll();

            return this.View(articlesCollection);
        }
	}
}