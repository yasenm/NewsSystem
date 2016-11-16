using System.Linq;

namespace NewsSystem.Data.Services.Contracts
{
    public interface ICategoryClientService
    {
        T GetById<T>(int id);
        IQueryable<T> GetAll<T>();
    }
}
