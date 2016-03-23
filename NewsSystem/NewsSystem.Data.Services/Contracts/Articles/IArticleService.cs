namespace NewsSystem.Data.Services.Contracts
{
    using System.Collections.Generic;

    using ViewModels.Articles;

    public interface IArticleService
    {
        IEnumerable<ArticleViewModel> GetAll();

        IEnumerable<ArticleViewModel> Get(long categoryId);

        ArticleEditViewModel GetEditModel(long articleId);

        bool Create(ArticleCreateViewModel model);

        bool Edit(ArticleEditViewModel model);
    }
}
