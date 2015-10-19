namespace NewsSystem.Data.UnitOfWork
{
    using NewsSystem.Data.Common.Repository;
    using NewsSystem.Data.Models;

    public interface INewsSystemData
    {
        INewsSystemDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Article> Articles { get; }

        int SaveChanges();
    }
}
