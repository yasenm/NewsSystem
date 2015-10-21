namespace NewsSystem.Data.Migrations
{
    using NewsSystem.Common.RandomGenerators;
    using NewsSystem.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
