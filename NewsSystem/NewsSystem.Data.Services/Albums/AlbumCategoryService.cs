namespace NewsSystem.Data.Services.Albums
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.AlbumCategories;

    public class AlbumCategoryService : IDataService, IAlbumCategoryService
    {
        public INewsSystemData Data { get; set; }

        public AlbumCategoryService(INewsSystemData data)
        {
            this.Data = data;
        }

        public IEnumerable<AlbumCategoryViewModel> GetAll()
        {
            var collection = this.Data.AlbumCategories
                .All().Where(ac => ac.ParentId == null).ToList();
            var other = collection
                .AsQueryable()
                //.Where(ac => ac.ParentId == null)
                .Project()
                .To<AlbumCategoryViewModel>()
                .ToList();

            return other;
        }
    }
}
