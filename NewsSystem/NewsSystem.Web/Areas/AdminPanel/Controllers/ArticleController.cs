namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Data.Services.Contracts;
    using Data.Services.Contracts.Category;
    using Data.ViewModels.Articles;
    using NewsSystem.Web.Areas.AdminPanel.Controllers.Base;

    using PagedList;

    using System.Linq;
    using System.Web.Mvc;

    using Grid.Mvc.Ajax.GridExtensions;
    using Web.Helpers.Contracts;

    public class ArticleController : AdminBaseController
    {
        private IArticleService ArticleService;
        private ICategoryService CategoryService;
        private IGridMvcHelper GridMvcHelper;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IGridMvcHelper gmHelper)
        {
            this.ArticleService = articleService;
            this.CategoryService = categoryService;
            this.GridMvcHelper = gmHelper;
        }

        public ActionResult Index()
        {
            return this.View();
        }
        
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
        
        public ActionResult ArticlesTable()
        {
            var collection = this.ArticleService.GetAll();
            return this.PartialView(collection);
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

            this.ViewBag.SelectedCoverImageId = model.CoverImageId;
            this.ViewBag.SelectedRelatedAlbumId = model.RelatedAlbumId;

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.ArticleService.Edit(model))
                {
                    return this.RedirectToAction("Index");
                }
            }
            this.ViewBag.Error = "Unable to save!";
            return this.View(model);
        }
        
        public ActionResult Delete(long articleId)
        {
            if (true)
            {
                return this.Content("Delete successfull!");
            }
            return this.RedirectToRoute(this.HttpContext.Request.Url.AbsolutePath, new { });
        }
    }
}