namespace NewsSystem.Data.Services.Albums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Models;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.Albums;

    public class AlbumService : IDataService, IAlbumService
    {
        public INewsSystemData Data { get; set; }
        private INSImageService NSImageService { get; set; }

        public AlbumService(INewsSystemData data, INSImageService nsiService)
        {
            this.Data = data;
            this.NSImageService = nsiService;
        }

        public AlbumEditViewModel GetAlbumForEdit(long albumId)
        {
            var album = this.Data.Albums.GetById(albumId);
            var model = Mapper.Map<AlbumEditViewModel>(album);

            return model;
        }

        public bool EditAlbum(AlbumEditViewModel editModel)
        {
            try
            {
                //Album editAlbum = Mapper.Map<Album>(editModel);
                Album editAlbum = this.Data.Albums.GetById(editModel.Id);
                this.NSImageService.SaveImagesToAlbum(editModel.AlbumPostedImages, editAlbum.Id);
                editAlbum.Text = editModel.Text;
                editAlbum.Name = editModel.Name;

                this.Data.Albums.Update(editAlbum);
                this.Data.SaveChanges();

                this.SaveTokensToAlbum(editAlbum, editModel.Tokens);
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
                this.Data.Albums.Add(album);
                this.Data.SaveChanges();

                this.SaveTokensToAlbum(album, albumModel.Tokens);
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
                this.Data.Albums.Delete(albumId);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<AlbumGridViewModel> GetAlbums()
        {
            var collection = this.Data.Albums.All()
                .ToList()
                .AsQueryable()
                .Project()
                .To<AlbumGridViewModel>()
                .ToList();

            return collection;
        }

        public IEnumerable<AlbumGridViewModel> GetAlbumsBySearchText(string searchText, string tags)
        {
            var queryableAlbums = this.Data.Albums.All();
            if (searchText != null && searchText != string.Empty)
            {
                queryableAlbums = queryableAlbums.Where(a => a.Name.ToLower().Contains(searchText));
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
            var result = this.Data.Albums.All()
                .Where(a => a.AlbumCategoryId == categoryId)
                .ToList()
                .AsQueryable()
                .Project()
                .To<AlbumGridViewModel>()
                .ToList();

            return result;
        }

        private void SaveTokensToAlbum(Album album, ICollection<string> chosenTokens)
        {
            if (chosenTokens.Count > 0)
            {
                var tokens = chosenTokens.ToList()[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                album.Tags.Clear();
                this.Data.SaveChanges();

                foreach (var token in tokens)
                {
                    var dbTag = this.Data.Tags
                        .All()
                        .FirstOrDefault(tnsi => tnsi.Name.ToLower() == token.ToLower());

                    if (dbTag == null)
                    {
                        dbTag = new Tag
                        {
                            Name = token,
                        };

                        this.Data.Tags.Add(dbTag);
                        this.Data.SaveChanges();
                    }

                    dbTag.Albums.Add(album);
                    album.Tags.Add(dbTag);
                    this.Data.Tags.Update(dbTag);
                    this.Data.Albums.Update(album);
                    this.Data.SaveChanges();
                }
            }
        }
    }
}
