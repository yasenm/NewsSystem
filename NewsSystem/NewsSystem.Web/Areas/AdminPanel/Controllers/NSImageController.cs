namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.NSImages;
    using Data.ViewModels.NSImages;
    using PagedList;
    using Common.Constants;

    public class NSImageController : Controller
    {
        private INSImageService NSImageService;

        public NSImageController(INSImageService nsiService)
        {
            this.NSImageService = nsiService;
        }

        public ActionResult Index(int page = 1)
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult NSImagesAlbumGrid(long albumId)
        {
            var collection = this.NSImageService.GetAlbumImagesIds(albumId);
            return this.PartialView("NSImagesAlbumGrid", collection);
        }

        [HttpGet]
        public ActionResult NSImagesGrid(int page = 1)
        {
            if (page > 0)
            {
                var nsPicturesCollection = this.NSImageService.GetImages();

                var model = new PagedList<NSImageGridViewModel>(nsPicturesCollection, page, NSImagesConstants.PageSize);

                return this.PartialView(model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult MakeAlbumCover(long imageId, long albumId)
        {
            var changed = this.NSImageService.ChangeAlbumCoverByImageId(imageId, albumId);
            return this.NSImagesAlbumGrid(albumId);
        }

        [HttpGet]
        public ActionResult Edit(long nsImageId)
        {
            NSImageEditViewModel model = this.NSImageService.GetImageForEdit(nsImageId);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NSImageEditViewModel model)
        {
            return null;
        }
    }
}