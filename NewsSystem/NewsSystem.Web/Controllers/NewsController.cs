namespace NewsSystem.Web.Controllers
{
    using Data.Services.Contracts;
    using Base;

    using System.Linq;
    using System.Web.Mvc;
    using Data.ViewModels.Articles;

    public class NewsController : BaseController
    {
        private IArticleClientService _newsService;

        public NewsController(IArticleClientService newsService)
        {
            _newsService = newsService;
        }

        // GET: News
        public ActionResult RecentNews()
        {
            var result = _newsService.GetAllGeneric<NewsOverviewClientViewModel>()
                .Take(5)
                .ToList();

            return PartialView(result);
        }

        public ActionResult PopularNews()
        {
            var result = _newsService.GetAllGeneric<NewsPopularOverviewClientViewModel>()
                .Take(4)
                .ToList();

            return PartialView(result);
        }

        public ActionResult EditorsChoiceNews()
        {
            var result = _newsService.GetAllGeneric<NewsEditorsChoiceOverviewClientViewModel>()
                .Take(4)
                .ToList();

            return PartialView(result);
        }

        public ActionResult Details(long id)
        {
            var model = _newsService.GetById(id);
            return View(model);
        }

        //public ActionResult Details(string title)
        //{
        //    return View(_newsService.GetByTitle(title));
        //}
    }
}