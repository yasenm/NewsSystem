namespace NewsSystem.Data.Services.Albums
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Category;
    using NewsSystem.Data.UnitOfWork;
    using NewsSystem.Data.ViewModels.Categories;

    public class CategoryService : IDataService, ICategoryService
    {
        public INewsSystemData Data { get; set; }

        public CategoryService(INewsSystemData data)
        {
            this.Data = data;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var collection = this.Data.Categories.All()
                .Where(ac => ac.ParentId == null)
                .ToList()
                .AsQueryable()
                .Project()
                .To<CategoryViewModel>()
                .ToList();

            return collection;
        }


        public IEnumerable<CategoryDDLViewModel> GetForDDLAll()
        {
            var collection = this.Data.Categories.All()
                .Project()
                .To<CategoryDDLViewModel>()
                .ToList();

            return collection;
        }
    }
}
