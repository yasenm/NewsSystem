namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.Albums;

    public class AlbumCategoryController : Controller
    {
        private IAlbumCategoryService AlbumCategoryService { get; set; }

        public AlbumCategoryController(IAlbumCategoryService acService)
        {
            this.AlbumCategoryService = acService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult AlbumCategoryTree()
        {
            var model = this.AlbumCategoryService.GetAll();
            return this.PartialView("AlbumCategoryTree", model);
        }
	}
}