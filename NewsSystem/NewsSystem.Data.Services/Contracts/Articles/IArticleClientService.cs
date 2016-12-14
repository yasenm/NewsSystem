namespace NewsSystem.Data.Services.Contracts
{
    using System.Linq;

    using ViewModels.Articles;

    public interface IArticleClientService
    {
        IQueryable<NewsOverviewClientViewModel> GetAll();
        IQueryable<T> GetAllGeneric<T>();
        IQueryable<T> GetAllByCategoryName<T>(string catName);
        IQueryable<T> GetAllByCategoryId<T>(int id);
        IQueryable<T> GetAllByTagName<T>(string name);
        IQueryable<T> GetAllByTagId<T>(long id);
        T GetById<T>(long id);
        T GetByTitle<T>(string title);
        void UpdateVisitorIp(long id, string userHostAddress);
    }
}
