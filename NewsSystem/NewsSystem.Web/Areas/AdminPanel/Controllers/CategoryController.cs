namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.Category;
    using Base;

    public class CategoryController : AdminBaseController
    {
        private ICategoryService CategoryService { get; set; }

        public CategoryController(ICategoryService cService)
        {
            this.CategoryService = cService;
        }

        public ActionResult CategoryTree(string Action, string Controller, string UpdateTargetId)
        {
            ViewBag.Action = Action;
            ViewBag.Controller = Controller;
            ViewBag.UpdateTargetId = UpdateTargetId;

            var model = CategoryService.GetAll();
            return PartialView("CategoryTree", model);
        }

        public ActionResult CategoryTreeSelectMode(string Action, string Controller, string UpdateTargetId)
        {
            ViewBag.Action = Action;
            ViewBag.Controller = Controller;
            ViewBag.UpdateTargetId = UpdateTargetId;

            var model = CategoryService.GetAll();
            return PartialView("CategoryTreeSelectMode", model);
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SortableList()
        {
            var model = CategoryService.GetAll();
            return PartialView(model);
        }
    }
}