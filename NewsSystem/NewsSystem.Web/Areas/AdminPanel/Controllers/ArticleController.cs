namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Data.Services.Contracts;
    using Data.ViewModels.Articles;

    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;

    using System.Web.Mvc;

    public class ArticleController : AdminBaseController
    {
        private IArticleService ArticleService;

        public ArticleController(IArticleService articleService)
        {
            this.ArticleService = articleService;
        }

        public ActionResult Index()
        {
            var articlesCollection = this.ArticleService.GetAll();

            return this.View(articlesCollection);
        }

        [HttpGet]
        public ActionResult Edit(long articleId)
        {
            var model = this.ArticleService.GetEditModel(articleId);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.ArticleService.Edit(model))
                    return this.View(model);
            }
            return this.View(model);
        }
    }
}