namespace NewsSystem.Data.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Articles;

    public interface IArticleService
    {
        IQueryable<ArticleViewModel> GetAll();

        IEnumerable<ArticleViewModel> Get(long categoryId);

        ArticleEditViewModel GetEditModel(long articleId);

        bool Create(ArticleCreateViewModel model);

        bool Edit(ArticleEditViewModel model);
    }
}
