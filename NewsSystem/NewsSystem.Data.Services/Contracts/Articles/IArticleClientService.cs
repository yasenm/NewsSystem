namespace NewsSystem.Data.Services.Contracts
{
    using System.Linq;

    using ViewModels.Articles;

    public interface IArticleClientService
    {
        IQueryable<NewsOverviewClientViewModel> GetAll();
        IQueryable<T> GetAllGeneric<T>();
        NewsDetailsClientViewModel GetById(long id);
        NewsDetailsClientViewModel GetByTitle(string title);
    }
}
