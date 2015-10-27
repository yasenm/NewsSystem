namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.NSImages;

    public class NSImageController : Controller
    {
        private INSImageService NSImageService;

        public NSImageController(INSImageService nsiService)
        {
            this.NSImageService = nsiService;
        }

        [HttpGet]
        public ActionResult NSImagesAlbumGrid(long albumId)
        {
            var collection = this.NSImageService.GetAlbumImagesIds(albumId);
            return this.PartialView("NSImagesAlbumGrid", collection);
        }

        [HttpGet]
        public ActionResult NSImage(long imageId)
        {
            var imgModel = this.NSImageService.GetImageById(imageId);
            return this.File(imgModel.ByteContent, "image/gif");
        }


        [HttpPost]
        public ActionResult MakeAlbumCover(long imageId, long albumId)
        {
            var changed = this.NSImageService.ChangeAlbumCoverByImageId(imageId, albumId);
            return this.NSImagesAlbumGrid(albumId);
        }
    }
}