using NewsSystem.Data.Services.Contracts;
using NewsSystem.Data.ViewModels.Categories;
using NewsSystem.Web.Controllers.Base;

using System.Linq;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private ICategoryClientService _catService;

        public CategoryController(ICategoryClientService catService)
        {
            _catService = catService;
        }

        // GET: Categories
        public ActionResult TopNavbar()
        {
            var result = _catService.GetAll<CategoryMenuClientViewModel>().ToList();

            return PartialView(result);
        }
    }
}