namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;
    using System.Collections.Generic;

    using NewsSystem.Data.Services.Contracts.Category;
    using Base;
    using Data.ViewModels.Categories;
    using System.Web;
    using NewsSystem.Common.Constants;

    //[Authorize(Roles = UsersConstants.AdminRole)]
    public class CategoryController : AdminBaseController
    {
        private ICategoryService _categoryService { get; set; }

        public CategoryController(ICategoryService cService)
        {
            this._categoryService = cService;
        }

        public ActionResult CategoryTree(string Action, string Controller, string UpdateTargetId)
        {
            ViewBag.Action = Action;
            ViewBag.Controller = Controller;
            ViewBag.UpdateTargetId = UpdateTargetId;

            var model = _categoryService.GetAll();
            return PartialView("CategoryTree", model);
        }

        public ActionResult CategoryTreeSelectMode(string Action, string Controller, string UpdateTargetId)
        {
            ViewBag.Action = Action;
            ViewBag.Controller = Controller;
            ViewBag.UpdateTargetId = UpdateTargetId;

            var model = _categoryService.GetAll();
            return PartialView("CategoryTreeSelectMode", model);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new CategoryFormViewModel
            {
                Categories = _categoryService.GetDDL()
            };
            return PartialView("Form", model);
        }
        
        [HttpPost]
        public ActionResult Form(CategoryFormViewModel model)
        {
            return SortableList();
        }

        public ActionResult SortableList()
        {
            var model = _categoryService.GetAll();
            return PartialView("SortableList", model);
        }

        [HttpPost]
        public ActionResult UpdateList(List<OrderedCategoryViewModel> model)
        {
            if (_categoryService.UpdateCategoryList(model))
            {
                return SortableList();
            }
            throw new HttpException(400, "Could not save!");
        }

        [HttpPost]
        public ActionResult EditName(long id, string newName)
        {
            if (_categoryService.EditCatName(id, newName))
            {
                return SortableList();
            }
            throw new HttpException(400, "Could not save!");
        }
    }
}