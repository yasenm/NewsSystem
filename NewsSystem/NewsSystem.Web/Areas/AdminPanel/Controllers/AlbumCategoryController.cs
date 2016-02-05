namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.Albums;
    using Base;

    public class AlbumCategoryController : AdminBaseController
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