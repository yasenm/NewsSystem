using System.Linq;
using NewsSystem.Data.Services.Contracts;
using NewsSystem.Data.UnitOfWork;
using AutoMapper.QueryableExtensions;
using System;
using AutoMapper;

namespace NewsSystem.Data.Services.Category
{
    public class CategoryClientService : ICategoryClientService
    {
        private INewsSystemData _data;

        public CategoryClientService(INewsSystemData data)
        {
            _data = data;
        }

        public IQueryable<T> GetAll<T>()
        {
            var result = _data.Categories.All()
                .Where(c => c.ParentId == null)
                .ToList()
                .AsQueryable()
                .Project()
                .To<T>();

            return result;
        }

        public T GetById<T>(int id)
        {
            var result = _data.Categories.All().FirstOrDefault(c => c.Id == id);

            if (result != null)
            {
                var model = Mapper.Map<T>(result);
                return model;
            }
            return default(T);
        }
    }
}
