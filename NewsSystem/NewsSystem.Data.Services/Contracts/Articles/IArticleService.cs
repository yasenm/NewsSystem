namespace NewsSystem.Data.Services.Contracts
{
    using System.Collections.Generic;

    using ViewModels.Articles;

    public interface IArticleService
    {
        IEnumerable<ArticleViewModel> GetAll();
    }
}
