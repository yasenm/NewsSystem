using System.Linq;

namespace NewsSystem.Data.Services.Contracts
{
    public interface ICategoryClientService
    {
        IQueryable<T> GetAll<T>();
    }
}
