namespace NewsSystem.Data.Services.Articles
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.ViewModels.Articles;
    using NewsSystem.Data.UnitOfWork;
    using System;
    using Contracts.Category;
    using Models;
    public class ArticleService : IArticleService, IDataService
    {
        public INewsSystemData _data { get; set; }
        private ICategoryService CategoryService { get; set; }
        private ITagsService TagsService { get; set; }

        public ArticleService(INewsSystemData data, ICategoryService categoryService, ITagsService tagsService)
        {
            this._data = data;
            this.CategoryService = categoryService;
            this.TagsService = tagsService;
        }

        public IQueryable<T> GetAll<T>()
        {
            var collection = this._data.Articles
                .All()
                .OrderBy(a => a.CreatedOn)
                .Project()
                .To<T>();

            return collection;
        }

        public IEnumerable<T> Get<T>(long categoryId)
        {
            var collection = this._data.Articles
                .All()
                .Where(a => a.Categories.FirstOrDefault(c => c.Id == categoryId) != null)
                .Project()
                .To<T>()
                .ToList();

            return collection;
        }

        public T GetEditModel<T>(long articleId)
        {
            var article = this._data.Articles.GetById(articleId);
            var model = Mapper.Map<T>(article);
            return model;
        }

        public bool Edit(ArticleEditViewModel model)
        {
            try
            {
                //var article = Mapper.Map<Article>(model);
                var article = this._data.Articles.GetById(model.Id);
                article.Title = model.Title;
                article.Description = model.Description;
                article.Summary = model.Summary;
                article.CoverImageId = model.CoverImageId;
                article.RelatedAlbumId = model.RelatedAlbumId;
                article.IsMain = model.IsMain;
                article.IsTopMain = model.IsTopMain;

                this.CategoryService.SaveCategorableEntityToCategories(article, model.ChosenCategories);
                this.TagsService.SaveTagsToTagableEntity(article, model.ChosenTags);

                this._data.Articles.Update(article);
                this._data.SaveChanges();
                if (article.IsTopMain)
                {
                    this.RemoveTopMainsAndKeepUpdated(article.Id);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

        private void RemoveTopMainsAndKeepUpdated(long id)
        {
            var topMainArticles = this._data.Articles.All().Where(a => a.IsTopMain && a.Id != id).ToList();
            foreach (var article in topMainArticles)
            {
                article.IsTopMain = false;
                this._data.Articles.Update(article);
                this._data.SaveChanges();
            }
        }

        public bool Create(ArticleCreateViewModel model, string userName)
        {
            try
            {
                var user = _data.Users.All().FirstOrDefault(u => u.UserName == userName).Id;
                var article = Mapper.Map<Article>(model);
                article.AuthorId = user;

                this._data.Articles.Add(article);
                this._data.SaveChanges();

                foreach (var parent in model.ChosenCategories)
                {
                    this.CategoryService.SaveCategorableToCategories(article, parent.Children.Where(c => c.IsChecked).Select(c => c.Id).ToList());
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

        public bool Delete(long articleId)
        {
            try
            {
                this._data.Articles.Delete(articleId);
                this._data.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
