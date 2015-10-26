namespace NewsSystem.Data.Services.Albums
{
    using System;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Models;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.Albums;
    using System.Collections.Generic;

    public class AlbumService : IDataService, IAlbumService
    {
        public INewsSystemData Data { get; set; }

        public AlbumService(INewsSystemData data)
        {
            this.Data = data;
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
                this.Data.Albums.Update(editAlbum);
                this.Data.SaveChanges();

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
    }
}
