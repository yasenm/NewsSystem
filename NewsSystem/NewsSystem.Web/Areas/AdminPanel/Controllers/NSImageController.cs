﻿namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using PagedList;

    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Data.ViewModels.NSImages;
    using NewsSystem.Common.Constants;
    using Base;

    public class NSImageController : AdminBaseController
    {
        private INSImageService NSImageService;
        private ITokenNSImageService TokenNSImageService;

        public NSImageController(INSImageService nsiService, ITokenNSImageService tokenService)
        {
            this.NSImageService = nsiService;
            this.TokenNSImageService = tokenService;
        }

        public ActionResult Index(int page = 1)
        {
            return this.View();
        }

        public ActionResult NSImagesAlbumGrid(long albumId)
        {
            var collection = this.NSImageService.GetAlbumImagesIds(albumId);

            this.ViewBag.AlbumId = albumId;
            return this.PartialView("NSImagesAlbumGrid", collection);
        }

        public ActionResult NSImagesAlbumChooseGrid(long albumId, string text, string tags)
        {
            var collection = this.NSImageService.GetImagesToChoose(text.Trim().ToLower(), tags, albumId);

            this.ViewBag.AlbumId = albumId;
            this.ViewBag.LastText = text;
            this.ViewBag.LastTags = tags;
            return this.PartialView("NSImagesAlbumChooseGrid", collection);
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
            if (this.ModelState.IsValid)
            {
                this.NSImageService.EditImage(model);

                return this.Edit(model.Id); // this.RedirectToAction("Index"); // this.Index();
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult GetImageTokens()
        {
            var stringTokens = this.TokenNSImageService.GetFullListOfTokens().Select(m => m.Name);
            return Json(stringTokens, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveFromAlbum(long imgId, long albumId)
        {
            this.NSImageService.RemoveFromAlbum(imgId, albumId);
            return null;
        }

        public ActionResult Create()
        {
            var model = new NSImageCreateViewModel();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(NSImageCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.NSImageService.SaveImageToDB(model);
            }

            return null;
        }

        [HttpPost]
        public ActionResult PushImagesToAlbum(long albumId, long[] imagesIds)
        {
            var result = this.NSImageService.PushImagesToAlbum(albumId, imagesIds);
            return this.NSImagesAlbumGrid(albumId);
        }
    }
}