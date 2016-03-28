namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Data.Services.Contracts;
    using Data.Services.Contracts.Category;
    using Data.ViewModels.Articles;

    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;
    using PagedList;
    using System.Linq;
    using System.Web.Mvc;

    public class ArticleController : AdminBaseController
    {
        private IArticleService ArticleService;
        private ICategoryService CategoryService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService)
        {
            this.ArticleService = articleService;
            this.CategoryService = categoryService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult ArticlesList(long? categoryId, int page = 1)
        {
            this.ViewBag.CategoryId = categoryId;
            if (categoryId != null)
            {
                var collection = new PagedList<ArticleViewModel>(this.ArticleService.Get((long)categoryId), page, 5);
                return this.PartialView(collection);
            }
            var model = new PagedList<ArticleViewModel>(this.ArticleService.GetAll(), page, 5);
            return this.PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ArticleCreateViewModel();
            model.ChosenCategories = this.CategoryService.GetAllCheckbox().ToList();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.ArticleService.Create(model);
                return this.View(model);
            }


            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(long articleId)
        {
            var model = this.ArticleService.GetEditModel(articleId);
            model.ChosenCategories = this.CategoryService.GetAllCheckbox().ToList();

            foreach (var chosenCat in model.ChosenCategories)
            {
                foreach (var cat in chosenCat.Children)
                {
                    if (model.CategoriesIds.Contains(cat.Id))
                    {
                        cat.IsChecked = true;
                    }
                }
            }

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