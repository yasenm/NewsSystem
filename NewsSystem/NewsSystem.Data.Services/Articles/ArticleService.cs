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

    public class ArticleService : IArticleService, IDataService
    {
        public INewsSystemData Data { get; set; }

        public ArticleService(INewsSystemData data)
        {
            this.Data = data;
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

        public ArticleEditViewModel GetEditModel(long articleId)
        {
            var model = Mapper.Map<ArticleEditViewModel>(this.Data.Articles.GetById(articleId));
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
    }
}
