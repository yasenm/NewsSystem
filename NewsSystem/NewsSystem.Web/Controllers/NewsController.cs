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
    using System.Web;
    using NewsSystem.Common.Helpers;
    using System;

    public class NewsController : BaseController
    {
        private const string VIEWS_COUNT_COOKIE_KEY = "V_CTNS";

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
            //_newsService.UpdateVisitorIp(id, HttpContext.Request.UserHostAddress);
            AddOrCheckViewCookie(id);
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

        private void AddOrCheckViewCookie(long newsId)
        {
            var cookie = GetCookie(VIEWS_COUNT_COOKIE_KEY);
            string value;
            if (cookie == null)
            {
                SetCookie(VIEWS_COUNT_COOKIE_KEY, newsId.ToString());
                _newsService.UpdateViewsCount(newsId);
            }
            else
            {
                var currentVal = GetCookieVal(VIEWS_COUNT_COOKIE_KEY);
                var viewdNewsIds = currentVal
                    .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                if (!viewdNewsIds.Contains(newsId.ToString()))
                {
                    viewdNewsIds.Add(newsId.ToString());
                    value = string.Join(",", viewdNewsIds);
                    SetCookie(VIEWS_COUNT_COOKIE_KEY, value);
                    _newsService.UpdateViewsCount(newsId);
                }
            }
        }
    }
}