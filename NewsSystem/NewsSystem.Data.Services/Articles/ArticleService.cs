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
        public INewsSystemData Data { get; set; }
        private ICategoryService CategoryService { get; set; }
        private ITagsService TagsService { get; set; }

        public ArticleService(INewsSystemData data, ICategoryService categoryService, ITagsService tagsService)
        {
            this.Data = data;
            this.CategoryService = categoryService;
            this.TagsService = tagsService;
        }

        public IQueryable<ArticleViewModel> GetAll()
        {
            var collection = this.Data.Articles
                .All()
                .OrderBy(a => a.CreatedOn)
                .Project()
                .To<ArticleViewModel>();

            return collection;
        }

        public IEnumerable<ArticleViewModel> Get(long categoryId)
        {
            var collection = this.Data.Articles
                .All()
                .Where(a => a.Categories.FirstOrDefault(c => c.Id == categoryId) != null)
                .Project()
                .To<ArticleViewModel>()
                .ToList();

            return collection;
        }

        public ArticleEditViewModel GetEditModel(long articleId)
        {
            var article = this.Data.Articles.GetById(articleId);
            var model = Mapper.Map<ArticleEditViewModel>(article);
            return model;
        }

        public bool Edit(ArticleEditViewModel model)
        {
            try
            {
                //var article = Mapper.Map<Article>(model);
                var article = this.Data.Articles.GetById(model.Id);
                article.Title = model.Title;
                article.Description = model.Description;
                article.Summary = model.Summary;
                article.CoverImageId = model.CoverImageId;
                article.RelatedAlbumId = model.RelatedAlbumId;
                article.IsMain = model.IsMain;
                article.IsTopMain = model.IsTopMain;

                this.CategoryService.SaveCategorableEntityToCategories(article, model.ChosenCategories);
                this.TagsService.SaveTagsToTagableEntity(article, model.ChosenTags);

                this.Data.Articles.Update(article);
                this.Data.SaveChanges();
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
            var topMainArticles = this.Data.Articles.All().Where(a => a.IsTopMain && a.Id != id).ToList();
            foreach (var article in topMainArticles)
            {
                article.IsTopMain = false;
                this.Data.Articles.Update(article);
                this.Data.SaveChanges();
            }
        }

        public bool Create(ArticleCreateViewModel model)
        {
            try
            {
                var article = Mapper.Map<Article>(model);

                this.Data.Articles.Add(article);
                this.Data.SaveChanges();

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
    }
}
