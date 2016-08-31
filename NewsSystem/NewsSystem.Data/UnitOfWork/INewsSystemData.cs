﻿namespace NewsSystem.Data.UnitOfWork
{
    using Common.Models;
    using NewsSystem.Data.Common.Repository;
    using NewsSystem.Data.Models;

    public interface INewsSystemData
    {
        INewsSystemDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Article> Articles { get; }

        IRepository<Theme> Themes { get; }

        IRepository<Album> Albums { get; }

        IRepository<Category> Categories { get; }

        IRepository<Tag> Tags { get; }

        IRepository<NSImage> NSImages { get; }

        IRepository<Question> Questions { get; }

        IRepository<Answer> Answers { get; }

        int SaveChanges();
    }
}
