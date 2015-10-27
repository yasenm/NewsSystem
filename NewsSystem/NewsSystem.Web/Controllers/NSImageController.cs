namespace NewsSystem.Web.Controllers
{
    using System.Web.Mvc;

    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Web.Controllers.Base;

    public class NSImageController : BaseController
    {
        private INSImageService NSImageService;

        public NSImageController(INSImageService nsiService)
        {
            this.NSImageService = nsiService;
        }

        public ActionResult NSImage(long imageId)
        {
            var imageModel = this.NSImageService.GetImageById(imageId);
            return this.File(imageModel.ByteContent, "image/gif");
        }
    }
}