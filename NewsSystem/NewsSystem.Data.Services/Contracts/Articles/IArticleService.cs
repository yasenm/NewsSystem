namespace NewsSystem.Data.Services.Contracts
{
    using System.Collections.Generic;

    using NewsSystem.Data.ViewModels.Articles;

    public interface IArticleService
    {
        IEnumerable<ArticleViewModel> GetAll();
    }
}
