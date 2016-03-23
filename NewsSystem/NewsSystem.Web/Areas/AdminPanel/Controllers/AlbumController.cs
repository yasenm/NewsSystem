﻿namespace NewsSystem.Web.Areas.AdminPanel.Controllers
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
        private ICategoryService CategoryService;

        public AlbumController(IAlbumService albumService, ICategoryService categoryService)
        {
            this.AlbumService = albumService;
            this.CategoryService = categoryService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AlbumsGrid(long? categoryId)
        {
            if (categoryId != null)
            {
                return this.PartialView(this.AlbumService.GetAlbumsByCategoryId((long)categoryId));
            }
            return this.PartialView(this.AlbumService.GetAlbums());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AlbumCreateViewModel();
            model.ChosenCategories = this.CategoryService.GetAllCheckbox().ToList();

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
            model.ChosenCategories = this.CategoryService.GetAllCheckbox().ToList();

            foreach (var chosenCat in model.ChosenCategories)
            {
                foreach (var cat in chosenCat.Children)
                {
                    if (model.CategoriesIds.Contains(cat.Id))
                    {
                        cat.IsChecked = true;
                    }
                }
            }

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
                return this.AlbumsGrid(null);
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
        public ActionResult GetAlbumTokens()
        {
            //var stringTokens = this.AlbumTokenService.GetFullListOfTokens().Select(m => m.Name);
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}