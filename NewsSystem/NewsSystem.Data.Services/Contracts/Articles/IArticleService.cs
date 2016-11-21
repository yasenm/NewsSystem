namespace NewsSystem.Data.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Articles;

    public interface IArticleService
    {
        IQueryable<T> GetAll<T>();

        IEnumerable<T> Get<T>(long categoryId);

        T GetEditModel<T>(long articleId);

        bool Create(ArticleCreateViewModel model, string userName);

        bool Edit(ArticleEditViewModel model);

        bool Delete(long articleId);
    }
}
