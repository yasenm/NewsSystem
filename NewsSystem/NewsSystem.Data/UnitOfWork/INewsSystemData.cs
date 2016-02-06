namespace NewsSystem.Data.UnitOfWork
{
    using NewsSystem.Data.Common.Repository;
    using NewsSystem.Data.Models;

    public interface INewsSystemData
    {
        INewsSystemDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Article> Articles { get; }

        IRepository<Album> Albums { get; }

        IRepository<AlbumCategory> AlbumCategories { get; }

        IRepository<AlbumToken> AlbumTokens { get; }

        IRepository<NSImage> NSImages { get; }

        IRepository<TokenNSImage> TokensNSImages { get; }

        int SaveChanges();
    }
}
