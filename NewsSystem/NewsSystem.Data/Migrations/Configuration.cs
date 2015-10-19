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
