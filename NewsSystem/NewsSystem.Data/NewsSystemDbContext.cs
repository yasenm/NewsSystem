﻿namespace NewsSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using NewsSystem.Data.Common.Contracts.CodeFirstConvensions;
    using NewsSystem.Data.Common.Models;
    using NewsSystem.Data.Migrations;
    using NewsSystem.Data.Models;

    public class NewsSystemDbContext : IdentityDbContext<User>, INewsSystemDbContext
    {
        public NewsSystemDbContext()
            : base("NewsSystemConnectionString")
        {
            Database.SetInitializer<NewsSystemDbContext>(new MigrateDatabaseToLatestVersion<NewsSystemDbContext, Configuration>());
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<VisitorIp> VisitorsIps { get; set; }

        public IDbSet<Theme> Themes { get; set; }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<NSImage> NSImages { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public static NewsSystemDbContext Create()
        {
            return new NewsSystemDbContext();
        }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
