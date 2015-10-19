namespace NewsSystem.Data.Services.Articles
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.ViewModels;
    using NewsSystem.Data.UnitOfWork;

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
    }
}
