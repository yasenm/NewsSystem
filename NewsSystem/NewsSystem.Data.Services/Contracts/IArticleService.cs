namespace NewsSystem.Data.Services.Contracts
{
    using System.Collections.Generic;

    using NewsSystem.Data.ViewModels;

    public interface IArticleService
    {
        IEnumerable<ArticleViewModel> GetAll();
    }
}
