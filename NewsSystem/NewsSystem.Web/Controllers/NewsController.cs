namespace NewsSystem.Web.Controllers
{
    using Data.Services.Contracts;
    using Base;

    using System.Linq;
    using System.Web.Mvc;
    using Data.ViewModels.Articles;
    using System.Web.Http;
    using Data.Services.Contracts.Tags;
    using NewsSystem.Common.Extensions;
    using PagedList;
    using NewsSystem.Common.Constants;
    using Constants.Common;

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
                .Where(m => m.IsMain)
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
                .Take(5)
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

        public ActionResult ByCategory(int id, string title, int? page)
        {
            ViewBag.Title = $"{CommonSettings.SiteDefaultTitle} - {title}";
            ViewBag.CategoryTitle = title;
            ViewBag.CategoryId = id;

            var results = _newsService
                .GetAllByCategoryId<NewsOverviewClientViewModel>(id)
                .ToPagedList(page == null ? PagedListSettings.DefaultStartPage : (int)page, PagedListSettings.GlobalListCount);

            return View(results);
        }

        public ActionResult ByTagName([FromBody]long id, string name, int? page)
        {
            ViewBag.TagName = name;
            ViewBag.TagId = id;

            var results = _newsService
                .GetAllByTagId<NewsOverviewClientViewModel>(id)
                .ToPagedList(page == null ? PagedListSettings.DefaultStartPage : (int)page, PagedListSettings.GlobalListCount);

            return View(results);
        }

        //public ActionResult Details(string title)
        //{
        //    return View(_newsService.GetByTitle(title));
        //}
    }
}