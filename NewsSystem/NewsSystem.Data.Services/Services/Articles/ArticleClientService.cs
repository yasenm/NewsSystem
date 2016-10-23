using AutoMapper.QueryableExtensions;

using NewsSystem.Data.Services.Contracts;
using NewsSystem.Data.UnitOfWork;
using NewsSystem.Data.ViewModels.Articles;

using System.Linq;
using System;
using AutoMapper;

namespace NewsSystem.Data.Services.Articles
{
    public class ArticleClientService : IArticleClientService
    {
        private INewsSystemData _data;

        public ArticleClientService(INewsSystemData data)
        {
            _data = data;
        }

        public IQueryable<NewsOverviewClientViewModel> GetAll()
        {
            var result = _data.Articles.All()
                //.Where(a => a.IsPublished)
                .OrderByDescending(a => a.CreatedOn)
                .Project()
                .To<NewsOverviewClientViewModel>();

            return result;
        }

        public IQueryable<T> GetAllGeneric<T>()
        {
            var result = _data.Articles.All()
                //.Where(a => a.IsPublished)
                .OrderByDescending(a => a.CreatedOn)
                .Project()
                .To<T>();

            return result;
        }

        public NewsDetailsClientViewModel GetById(long id)
        {
            var newsDataModel = _data.Articles.GetById(id);
            var newsModel = Mapper.Map<NewsDetailsClientViewModel>(newsDataModel);

            return newsModel;
        }

        public NewsDetailsClientViewModel GetByTitle(string title)
        {
            var newsDataModel = _data.Articles.All().FirstOrDefault(a => a.Title.ToLower() == title.ToLower());
            var newsModel = Mapper.Map<NewsDetailsClientViewModel>(newsDataModel);

            return newsModel;
        }
    }
}
