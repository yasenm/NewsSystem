namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.Services.Contracts.Category;
    using NewsSystem.Data.ViewModels.Albums;
    using Base;

    public class AlbumController : AdminBaseController
    {
        private IAlbumService AlbumService;
        private ICategoryService AlbumCategoryService;

        public AlbumController(IAlbumService albumService, ICategoryService acService)
        {
            this.AlbumService = albumService;
            this.AlbumCategoryService = acService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AlbumsGrid()
        {
            var model = this.AlbumService.GetAlbums();
            return this.PartialView("AlbumsGrid", model);
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
                var updateSucceded = this.AlbumService.EditAlbum(editModel);
                if (updateSucceded)
                {
                    return this.View(editModel);
                }
            }

            return this.View(editModel);
        }

        [HttpPost]
        public ActionResult Delete(long albumId)
        {
            var deleted = this.AlbumService.Delete(albumId);
            if (deleted)
            {
                return this.AlbumsGrid();
            }

            return this.Content("Unable to do action!");
        }

        [HttpGet]
        public ActionResult Search(string searchText, string tags)
        {
            var model = this.AlbumService.GetAlbumsBySearchText(searchText, tags);
            return this.PartialView("AlbumsGrid", model);
        }

        [HttpGet]
        public ActionResult SearchByAlbumCategory(long categoryId)
        {
            var model = this.AlbumService.GetAlbumsByCategoryId(categoryId);
            return this.PartialView("AlbumsGrid", model);
        }

        [HttpGet]
        public ActionResult GetAlbumTokens()
        {
            //var stringTokens = this.AlbumTokenService.GetFullListOfTokens().Select(m => m.Name);
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}