namespace NewsSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using NewsSystem.Data.Models;

    public interface INewsSystemDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<NSImage> NSImages { get; set; }

        IDbSet<TokenNSImage> TokensNSImages { get; set; }

        IDbSet<Album> Albums { get; set; }

        IDbSet<AlbumCategory> AlbumCategories { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
