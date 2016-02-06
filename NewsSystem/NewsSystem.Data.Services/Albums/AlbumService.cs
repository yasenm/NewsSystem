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
                var editAlbum = Mapper.Map<Album>(editModel);
                this.NSImageService.SaveImagesToAlbum(editModel.AlbumPostedImages, editAlbum.Id);

                this.Data.Albums.Update(editAlbum);
                this.Data.SaveChanges();

                if (editModel.Tokens.Count > 0)
                {
                    var tokens = editModel.Tokens.ToList()[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    editAlbum.AlbumTokens.Clear();
                    this.Data.SaveChanges();

                    foreach (var token in tokens)
                    {
                        var dbToken = this.Data.AlbumTokens
                            .All()
                            .FirstOrDefault(tnsi => tnsi.Name.ToLower() == token.ToLower());

                        if (dbToken == null)
                        {
                            dbToken = new AlbumToken
                            {
                                Name = token,
                            };

                            this.Data.AlbumTokens.Add(dbToken);
                            this.Data.SaveChanges();
                        }

                        dbToken.Albums.Add(editAlbum);
                        editAlbum.AlbumTokens.Add(dbToken);
                        this.Data.AlbumTokens.Update(dbToken);
                        this.Data.Albums.Update(editAlbum);
                        this.Data.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
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

        public IEnumerable<AlbumGridViewModel> GetAlbumsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            var result = this.Data.Albums.All()
                .Where(a => a.Name.ToLower().Contains(searchText))
                .ToList()
                .AsQueryable()
                .Project()
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
    }
}
