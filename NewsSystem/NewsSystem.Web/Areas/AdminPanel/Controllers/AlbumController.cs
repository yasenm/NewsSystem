namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.ViewModels.Albums;

    public class AlbumController : Controller
    {
        private IAlbumService AlbumService;
        private IAlbumCategoryService AlbumCategoryService;

        public AlbumController(IAlbumService albumService, IAlbumCategoryService acService)
        {
            this.AlbumService = albumService;
            this.AlbumCategoryService = acService;
        }

        [HttpGet]
        public ActionResult AlbumsGrid()
        {
            var model = this.AlbumService.GetAlbums();
            return this.PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AlbumCreateViewModel();
            var categories = this.AlbumCategoryService.GetForDDLAll();
            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl.AddRange(new SelectList(categories, "Id", "Name"));
            this.ViewBag.Categories = new SelectList(ddl, "Value", "Text");

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumCreateViewModel createModel)
        {
            if (ModelState.IsValid)
            {
                if (this.AlbumService.Create(createModel))
                {
                    return this.RedirectToAction("Index", "AlbumCategory");
                }
            }

            return this.View(createModel);
        }

        [HttpGet]
        public ActionResult Edit(long albumId)
        {
            var model = this.AlbumService.GetAlbumForEdit(albumId);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlbumEditViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                if (this.AlbumService.EditAlbum(editModel))
                {
                    return this.RedirectToAction("Index", "AlbumCategory");
                }
            }

            return this.View(editModel);
        }
    }
}