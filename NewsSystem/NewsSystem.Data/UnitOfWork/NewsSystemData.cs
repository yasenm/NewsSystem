namespace NewsSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;

    using NewsSystem.Data.Common.Models;
    using NewsSystem.Data.Common.Repository;
    using NewsSystem.Data.Models;
    using NewsSystem.Data.Repositories;

    public class NewsSystemData : INewsSystemData
    {
        private IDictionary<Type, object> repositories;

        public NewsSystemData(INewsSystemDbContext context)
        {
            this.Context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public INewsSystemDbContext Context { get; set; }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Article> Articles
        {
            get
            {
                return this.GetDeletableEntityRepository<Article>();
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
                return this.GetDeletableEntityRepository<Album>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetDeletableEntityRepository<Category>();
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                return this.GetDeletableEntityRepository<Tag>();
            }
        }

        public IRepository<NSImage> NSImages
        {
            get
            {
                return this.GetDeletableEntityRepository<NSImage>();
            }
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.Context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.Context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeOfRepository];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                }
            }
        }
    }
}
