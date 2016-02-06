namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;

    public class ArticleController : AdminBaseController
    {
        // GET: AdminPanel/Article
        public ActionResult Index()
        {
            return View();
        }
    }
}