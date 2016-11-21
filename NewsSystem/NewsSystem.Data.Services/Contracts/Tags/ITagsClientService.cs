using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Data.Services.Contracts.Tags
{
    public interface ITagsClientService
    {
        T GetTagById<T>(long id);
        IQueryable<T> GetAll<T>();
        IEnumerable<T> GetAllGenericForArticle<T>(long artId);
    }
}
