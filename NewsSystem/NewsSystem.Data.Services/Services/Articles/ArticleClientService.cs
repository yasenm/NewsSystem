using AutoMapper.QueryableExtensions;

using NewsSystem.Data.Services.Contracts;
using NewsSystem.Data.UnitOfWork;
using NewsSystem.Data.ViewModels.Articles;

using System.Linq;
using System;
using AutoMapper;
using System.Linq.Expressions;
using NewsSystem.Data.Models;

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

        public IQueryable<T> GetAllByCategoryId<T>(int id)
        {
            var result = GetArticlesByCategoriesExpression<T>(c => c.Id == id);
            return result;
        }

        public IQueryable<T> GetAllByCategoryName<T>(string catName)
        {
            var result = GetArticlesByCategoriesExpression<T>(c => c.Title.ToLower() == catName);
            return result;
        }

        public IQueryable<T> GetAllByTagName<T>(string name)
        {
            return GetArticlesByTagsExpression<T>(t => t.Name.ToLower() == name);
        }

        public IQueryable<T> GetAllByTagId<T>(long id)
        {
            return GetArticlesByTagsExpression<T>(t => t.Id == id);
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

        public T GetById<T>(long id)
        {
            return GetSingleByExpression<T>(a => a.Id == id);
        }

        public T GetByTitle<T>(string title)
        {
            return GetSingleByExpression<T>(a => a.Title.ToLower() == title.ToLower());
        }

        // Private methods
        private T GetSingleByExpression<T>(Expression<Func<Article, bool>> func)
        {
            var article = _data.Articles.All().FirstOrDefault(func);
            if (article != null)
            {
                var newsModel = Mapper.Map<T>(article);
                return newsModel;
            }
            return default(T);
        }

        private IQueryable<T> GetArticlesByCategoriesExpression<T>(Expression<Func<Models.Category, bool>> func)
        {
            var result = _data.Articles.All()
                    .Where(a => a.Categories.AsQueryable().Where(func).FirstOrDefault() != null)
                    .OrderByDescending(a => a.CreatedOn)
                    .Project()
                    .To<T>();

            return result;
        }

        private IQueryable<T> GetArticlesByTagsExpression<T>(Expression<Func<Tag, bool>> func)
        {
            var result = _data.Articles.All()
                    .Where(a => a.Tags.AsQueryable().Where(func).FirstOrDefault() != null)
                    .OrderByDescending(a => a.CreatedOn)
                    .Project()
                    .To<T>();

            return result;
        }

        public void UpdateVisitorIp(long id, string userHostAddress)
        {
            try
            {
                var ipAddress = AddOrUpdateVisitorIp(userHostAddress);
                if (ipAddress != null)
                {
                    var article = _data.Articles.GetById(id);
                    article.VisitorsIps.Add(ipAddress);
                    _data.Articles.Update(article);
                    ipAddress.Articles.Add(article);
                    _data.VisitorsIps.Update(ipAddress);
                    _data.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        private VisitorIp AddOrUpdateVisitorIp(string userHostAddress)
        {
            try
            {
                var ip = _data.VisitorsIps.All().FirstOrDefault(vi => vi.IpAddress == userHostAddress);
                if (ip != null)
                {
                    return ip;
                }
                var ipAddress = new VisitorIp { IpAddress = userHostAddress, LastVisit = DateTime.Now };
                _data.VisitorsIps.Add(ipAddress);
                _data.SaveChanges();
                return ipAddress;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
