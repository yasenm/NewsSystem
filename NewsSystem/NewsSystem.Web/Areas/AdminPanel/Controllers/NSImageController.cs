﻿namespace NewsSystem.Web.Areas.AdminPanel.Controllers
{
    using Base;

    using NewsSystem.Common.Constants;

    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Data.ViewModels.NSImages;

    using PagedList;

    using System.Web.Mvc;

    public class NSImageController : AdminBaseController
    {
        private INSImageService NSImageService;

        [HttpGet]
        public ActionResult NSImage(long imageId)
        {
            return this.PartialView("Image", imageId);
        }

        public NSImageController(INSImageService nsiService)
        {
            this.NSImageService = nsiService;
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

        public ActionResult NSImagesAlbumChooseGrid(long albumId, string text, string tags, int page = 1)
        {
            var collection = this.NSImageService.GetImagesToChoose(text, tags, albumId);

            var model = new PagedList<NSImageGridViewModel>(collection, page, NSImagesConstants.PageSize);

            this.ViewBag.AlbumId = albumId;
            this.ViewBag.LastText = text;
            this.ViewBag.LastTags = tags;
            return this.PartialView(model);
        }

        [HttpGet]
        public ActionResult NSImagesGrid(string tags, string searchText, int page = 1)
        {
            if (page > 0)
            {
                var nsPicturesCollection = this.NSImageService.GetImages(searchText, tags);

                var model = new PagedList<NSImageGridViewModel>(nsPicturesCollection, page, NSImagesConstants.PageSize);

                return this.PartialView(model);
            }

            return HttpNotFound();
        }
        
        public ActionResult NSImagesCoverImageGrid(long? selectedId, string searchTags, string searchText, int page = 1)
        {
            this.ViewBag.SelectedCoverImageId = selectedId;

            if (page > 0)
            {
                var nsPicturesCollection = this.NSImageService.GetImages(searchText, searchTags);

                var model = new PagedList<NSImageGridViewModel>(nsPicturesCollection, page, PagedListSettings.GlobalListCount);

                return this.PartialView(model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult MakeAlbumCover(long imageId, long albumId)
        {
            var changed = this.NSImageService.ChangeAlbumCoverByImageId(imageId, albumId);
            if (changed)
            {
                this.ViewBag.AltText = "No cover photo for album found...";
                return this.NSImage(imageId);
            }

            return new HttpStatusCodeResult(404);
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

        [HttpPost]
        public ActionResult RemoveFromAlbum(long imgId, long albumId)
        {
            var removed = this.NSImageService.RemoveFromAlbum(imgId, albumId);
            if (removed)
            {
                return this.NSImagesAlbumGrid(albumId);
            }

            return new HttpStatusCodeResult(404);
        }

        [HttpGet]
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
                if (this.NSImageService.SaveImageToDB(model))
                {
                    return this.RedirectToAction("Index");
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public ActionResult PushImagesToAlbum(long albumId, long[] imagesIds)
        {
            var result = this.NSImageService.PushImagesToAlbum(albumId, imagesIds);
            return this.NSImagesAlbumGrid(albumId);
        }
    }
}