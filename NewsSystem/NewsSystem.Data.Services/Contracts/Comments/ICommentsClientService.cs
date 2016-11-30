using System.Collections.Generic;

namespace NewsSystem.Data.Services.Contracts.Comments
{
    public interface ICommentsClientService
    {
        bool AddOrUpdate<T>(T model);

        bool Delete(long id);

        IEnumerable<T> GetByNewsId<T>(long newsId);

        IEnumerable<T> GetByUser<T>(string username);
    }
}
