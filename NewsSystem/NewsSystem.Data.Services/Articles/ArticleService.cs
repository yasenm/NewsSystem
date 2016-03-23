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

        public ArticleService(INewsSystemData data, ICategoryService categoryService)
        {
            this.Data = data;
            this.CategoryService = categoryService;
        }

        public IEnumerable<ArticleViewModel> GetAll()
        {
            var collection = this.Data.Articles
                .All()
                .Project()
                .To<ArticleViewModel>()
                .ToList();

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
                var article = this.Data.Articles.GetById(model.Id);
                article.Title = model.Title;
                article.Description = model.Description;
                article.Summary = model.Summary;

                this.CategoryService.SaveCategorableEntityToCategories(article, model.ChosenCategories);

                this.Data.Articles.Update(article);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
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
