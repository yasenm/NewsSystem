﻿namespace NewsSystem.Data.Services.Albums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Models;
    using Contracts;
    using Contracts.Albums;
    using Contracts.Category;
    using Contracts.NSImages;
    using UnitOfWork;
    using ViewModels.Albums;

    public class AlbumService : IDataService, IAlbumService
    {
        public INewsSystemData _data { get; set; }
        private INSImageService NSImageService { get; set; }
        private ITagsService TagsService { get; set; }
        private ICategoryService CategoryService { get; set; }

        public AlbumService(INewsSystemData data, INSImageService nsiService, ITagsService tagsService, ICategoryService cService)
        {
            this._data = data;
            this.NSImageService = nsiService;
            this.TagsService = tagsService;
            this.CategoryService = cService;
        }

        public T GetAlbumForEdit<T>(long albumId)
        {
            var album = this._data.Albums.GetById(albumId);
            var model = Mapper.Map<T>(album);

            return model;
        }

        public bool EditAlbum(AlbumEditViewModel editModel)
        {
            try
            {
                //Album editAlbum = Mapper.Map<Album>(editModel);
                Album editAlbum = this._data.Albums.GetById(editModel.Id);
                this.NSImageService.SaveImagesToAlbum(editModel.AlbumPostedImages, editAlbum.Id);
                editAlbum.Description = editModel.Description;
                editAlbum.Title = editModel.Title;
                editAlbum.Summary = editModel.Summary;

                this._data.Albums.Update(editAlbum);
                this._data.SaveChanges();

                this.TagsService.SaveTagsToTagableEntity(editAlbum, editModel.Tags);
                this.CategoryService.SaveCategorableEntityToCategories(editAlbum, editModel.ChosenCategories);
                return true;
            }
            catch (Exception e)
            {
                var message = e.Message;
                var erMsg = message;
                return false;
            }
        }

        public bool Create(AlbumCreateViewModel albumModel)
        {
            try
            {
                var album = Mapper.Map<Album>(albumModel);
                this._data.Albums.Add(album);
                this._data.SaveChanges();

                this.TagsService.SaveTagsToTagableEntity(album, albumModel.Tags);
                this.CategoryService.SaveCategorableEntityToCategories(album, albumModel.ChosenCategories);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(long albumId)
        {
            try
            {
                this._data.Albums.Delete(albumId);
                this._data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<T> GetAlbums<T>()
        {
            var collection = this._data.Albums.All()
                .ToList()
                .AsQueryable()
                .Project()
                .To<T>()
                .ToList();

            return collection;
        }

        public IEnumerable<AlbumGridViewModel> GetAlbumsBySearchText(string searchText, string tags)
        {
            var queryableAlbums = this._data.Albums.All();
            if (searchText != null && searchText != string.Empty)
            {
                queryableAlbums = queryableAlbums.Where(a => a.Title.ToLower().Contains(searchText));
            }
            if (tags != null && tags != string.Empty)
            {
                string[] sTags = tags.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tagName in sTags)
                {
                    queryableAlbums = queryableAlbums.Where(a => a.Tags.FirstOrDefault(at => at.Name.Contains(tagName)) != null);
                }
            }

            var result = queryableAlbums.Project()
                .To<AlbumGridViewModel>()
                .ToList();

            return result;
        }

        public IEnumerable<AlbumGridViewModel> GetAlbumsByCategoryId(long categoryId)
        {
            var result = this._data.Albums.All()
                .Where(a => a.Categories.FirstOrDefault(c => c.Id == categoryId) != null)
                .ToList()
                .AsQueryable()
                .Project()
                .To<AlbumGridViewModel>()
                .ToList();

            return result;
        }
    }
}
