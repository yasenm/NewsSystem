namespace NewsSystem.Web.Controllers
{
    using System.Web.Mvc;

    using Data.Services.Contracts.NSImages;
    using Base;
    using System;

    public class NSImageController : BaseController
    {
        private INSImageService _nsImageService;

        public NSImageController(INSImageService nsiService)
        {
            _nsImageService = nsiService;
        }

        [HttpGet]
        public ActionResult NSImage(long imageId)
        {
            try
            {
                var imageModel = _nsImageService.GetImageById(imageId);
                return File(imageModel.ByteContent, "image/gif");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}