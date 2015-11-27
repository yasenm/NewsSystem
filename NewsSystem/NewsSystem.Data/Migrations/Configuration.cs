namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NewsSystem.Common.RandomGenerators;
    using NewsSystem.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(NewsSystemDbContext context)
        {
            this.ArticlesSeed(context);
            this.AlbumCategoriesSeed(context);
            this.AlbumsSeed(context);
            this.TokenNSImagesSeed(context);
        }

        private void TokenNSImagesSeed(NewsSystemDbContext context)
        {
            if (!context.TokensNSImages.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    var newNSImageToken = new TokenNSImage
                    {
                        Name = StringGenerator.RandomStringWithSpaces(5, 30),
                    };

                    context.TokensNSImages.AddOrUpdate(newNSImageToken);
                }

                context.SaveChanges();
            }
        }

        private void AlbumCategoriesSeed(NewsSystemDbContext context)
        {
            if (!context.AlbumCategories.Any())
            {
                // Add generated parent categories
                for (int i = 0; i < 5; i++)
                {
                    var albumCategory = new AlbumCategory();
                    albumCategory.Name = StringGenerator.RandomStringWithSpaces(5, 40);
                    albumCategory.Text = StringGenerator.RandomStringWithSpaces(35, 400);
                    albumCategory.ParentId = null;

                    context.AlbumCategories.AddOrUpdate(albumCategory);
                }

                context.SaveChanges();

                var parents = context.AlbumCategories.ToList();
                // Add generated child categories
                for (int i = 0; i < 30; i++)
                {
                    var albumCategory = new AlbumCategory();
                    albumCategory.Name = StringGenerator.RandomStringWithSpaces(5, 40);
                    albumCategory.Text = StringGenerator.RandomStringWithSpaces(35, 400);
                    albumCategory.ParentId = parents[NumberGenerator.RandomNumber(0, parents.Count - 1)].Id;

                    context.AlbumCategories.AddOrUpdate(albumCategory);
                }

                context.SaveChanges();
            }
        }

        private void AlbumsSeed(NewsSystemDbContext context)
        {
            if (!context.AlbumCategories.Any())
            {
                return;
            }

            if (!context.Albums.Any())
            {
                var albumCategories = context.AlbumCategories.Where(ac => ac.Parent != null).ToList();

                for (int i = 0; i < 50; i++)
                {
                    var newAlbum = new Album();
                    newAlbum.Name = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    newAlbum.Text = StringGenerator.RandomStringWithSpaces(400, 1500);
                    newAlbum.AlbumCategoryId = albumCategories[NumberGenerator.RandomNumber(0, albumCategories.Count - 1)].Id;

                    context.Albums.AddOrUpdate(newAlbum);
                }

                context.SaveChanges();
            }
        }

        private void ArticlesSeed(NewsSystemDbContext context)
        {
            if (!context.Articles.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    var article = new Article();
                    article.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    article.Content = StringGenerator.RandomStringWithSpaces(400, 1500);

                    context.Articles.AddOrUpdate(article);
                }
                context.SaveChanges();
            }
        }
    }
}
