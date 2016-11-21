using AutoMapper;
using AutoMapper.QueryableExtensions;
using NewsSystem.Data.Services.Contracts.Tags;
using NewsSystem.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsSystem.Data.Services.Services.Tags
{
    public class TagsClientService : ITagsClientService
    {
        private INewsSystemData _data;

        public TagsClientService(INewsSystemData data)
        {
            _data = data;
        }

        public T GetTagById<T>(long id)
        {
            var dataModel = _data.Tags.All().FirstOrDefault(t => t.Id == id);
            if (dataModel != null)
            {
                var result = Mapper.Map<T>(dataModel);
                return result;
            }
            return default(T);
        }

        public IQueryable<T> GetAll<T>()
        {
            var result = _data.Tags.All()
                .ProjectTo<T>();

            return result;
        }

        public IEnumerable<T> GetAllGenericForArticle<T>(long artId)
        {
            var result = _data.Tags.All()
                .Where(t => t.Articles.FirstOrDefault(a => a.Id == artId) != null)
                .Project()
                .To<T>()
                .ToList();

            return result;
        }
    }
}
