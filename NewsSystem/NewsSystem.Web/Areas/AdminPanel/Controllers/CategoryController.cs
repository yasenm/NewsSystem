namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.Category;
    using Base;

    public class CategoryController : AdminBaseController
    {
        private ICategoryService categoryService { get; set; }

        public CategoryController(ICategoryService cService)
        {
            this.categoryService = cService;
        }

        public ActionResult CategoryTree()
        {
            var model = this.categoryService.GetAll();
            return this.PartialView("CategoryTree", model);
        }
	}
}