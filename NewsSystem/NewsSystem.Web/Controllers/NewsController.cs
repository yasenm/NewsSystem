namespace NewsSystem.Web.Controllers
{
    using Data.Services.Contracts;
    using Base;

    using System.Linq;
    using System.Web.Mvc;
    using Data.ViewModels.Articles;
    using System.Web.Http;
    using Data.Services.Contracts.Tags;
    using Data.ViewModels.Tags;

    public class NewsController : BaseController
    {
        private IArticleClientService _newsService;
        private ITagsClientService _tagService;

        public NewsController(IArticleClientService newsService, ITagsClientService tagService)
        {
            _newsService = newsService;
            _tagService = tagService;
        }

        public ActionResult FeaturedNews()
        {
            var result = _newsService.GetAllGeneric<NewsEditorsChoiceOverviewClientViewModel>()
                .Take(4)
                .ToList();

            return PartialView(result);
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
            var model = _newsService.GetById<NewsDetailsClientViewModel>(id);
            return View(model);
        }

        public ActionResult ByCategory(int id, string title)
        {
            ViewBag.CategoryTitle = title;
            var results = _newsService
                //.GetAllByCategoryName<NewsOverviewClientViewModel>(title)
                .GetAllByCategoryId<NewsOverviewClientViewModel>(id)
                .Take(5)
                .ToList();

            return View(results);
        }

        public ActionResult ByTagName([FromBody]long id, string name)
        {
            ViewBag.TagName = _tagService.GetTagById<TagClientViewModel>(id).Name;

            var tagId = id;
            var results = _newsService.GetAllByTagId<NewsOverviewClientViewModel>(id)
                .ToList();

            return View(results);
        }

        //public ActionResult Details(string title)
        //{
        //    return View(_newsService.GetByTitle(title));
        //}
    }
}