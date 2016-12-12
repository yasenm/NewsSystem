using NewsSystem.Data.Services.Contracts;
using NewsSystem.Data.ViewModels.Articles;
using NewsSystem.Web.Constants.Common;
using NewsSystem.Web.Controllers.Base;
using NewsSystem.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class RssController : BaseController
    {
        private IArticleClientService _newsService;
        
        public RssController(IArticleClientService newsService)
        {
            _newsService = newsService;
        }

        public ActionResult News()
        {
            var items = _newsService.GetAllGeneric<NewsRssFeedViewModel>()
                .OrderByDescending(m => m.CreatedOn)
                .Take(30)
                .ToList()
                .Select(m => new SyndicationItem
                {
                    Title = new TextSyndicationContent(m.Title),
                    Summary = new TextSyndicationContent(m.Summary),
                    Content = new TextSyndicationContent(m.Description),
                    PublishDate = m.CreatedOn,
                    Id = m.Id.ToString(),
                    BaseUri = new Uri(Url.AbsoluteAction("Details", "News", new { id = m.Id })),
                })
                .ToList();

            var feed = new SyndicationFeed(CommonSettings.SiteDefaultTitle,
                CommonSettings.SiteDefaultDescription,
                null,
                Guid.NewGuid().ToString(),
                DateTime.Now);
            feed.Items = items;
            
            return new RssActionResult { Feed = feed };
        }

        // TODO: In future add logic for other type of feeds
    }
}