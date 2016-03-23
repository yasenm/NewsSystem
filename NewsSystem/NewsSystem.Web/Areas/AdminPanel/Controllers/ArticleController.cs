namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Data.Services.Contracts;
    using Data.Services.Contracts.Category;
    using Data.ViewModels.Articles;

    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;

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

        public ActionResult ArticlesList(long? categoryId)
        {
            if (categoryId != null)
            {
                return this.PartialView(this.ArticleService.Get((long)categoryId));
            }
            return this.PartialView(this.ArticleService.GetAll());
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