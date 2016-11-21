namespace NewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using NewsSystem.Common.Constants;
    using NewsSystem.Common.RandomGenerators;
    using NewsSystem.Data.Models;
    using NewsSystem.Data.UnitOfWork;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsSystemDbContext>
    {
        private UserManager<User> userManager;
        private UserStore<User> userStore;
        private INewsSystemData Data;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(NewsSystemDbContext context)
        {
            this.userStore = new UserStore<User>(context);
            this.userManager = new UserManager<User>(this.userStore);
            this.Data = new NewsSystemData(context);

            this.SeedUserRoles(context);
            this.SeedStartUsers(context);
            this.ArticlesSeed();
            this.AlbumCategoriesSeed();
            this.AlbumsSeed();
            this.TokenNSImagesSeed();
            this.QuestionsSeed();
        }

        private void QuestionsSeed()
        {
            if (!this.Data.Context.Questions.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    var question = new Question();
                    question.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    question.Description = StringGenerator.RandomStringWithSpaces(400, 1500);
                    question.Summary = StringGenerator.RandomStringWithSpaces(70, 250);

                    this.Data.Questions.Add(question);
                    this.Data.SaveChanges();

                    var answersCount = NumberGenerator.RandomNumber(1, 6);
                    for (int j = 0; j < answersCount; j++)
                    {
                        AnswerByQuestionIdSeed(question.Id);
                    }
                }

            }
        }

        private void AnswerByQuestionIdSeed(int questionId)
        {
            var answer = new Answer();
            answer.QuestionId = questionId;
            answer.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
            answer.Description = StringGenerator.RandomStringWithSpaces(400, 1500);
            answer.Summary = StringGenerator.RandomStringWithSpaces(70, 250);

            this.Data.Answers.Add(answer);
            this.Data.SaveChanges();
        }

        private void SeedUserRoles(NewsSystemDbContext context)
        {
            if (!context.Roles.Any())
            {
                foreach (var role in UsersConstants.UsersRoles)
                {
                    context.Roles.AddOrUpdate(new IdentityRole(role));
                }
                context.SaveChanges();
            }
        }

        private void SeedStartUsers(NewsSystemDbContext context)
        {
            if (!context.Users.Any())
            {
                this.SeedMasterAdmin(context);
                this.SeedAuthors(context);
                this.SeedBasicUsers(context);
            }
        }

        private void SeedMasterAdmin(NewsSystemDbContext context)
        {
            User admin = new User
            {
                UserName = UsersConstants.MasterAdminName,
                Email = UsersConstants.MasterAdminName + "@newssystem.com",
            };

            this.userManager.Create(admin, UsersConstants.MasterAdminPassword);
            this.userManager.AddToRole(admin.Id, UsersConstants.AdminRole);
            context.SaveChanges();
        }

        private void SeedAuthors(NewsSystemDbContext context)
        {
            this.SeedUsers(context, 10, UsersConstants.AuthorRole);
        }

        private void SeedBasicUsers(NewsSystemDbContext context)
        {
            this.SeedUsers(context, 40, UsersConstants.BasicUserRole);
        }

        private void SeedUsers(NewsSystemDbContext context, int usersCount, string role)
        {
            for (int i = 0; i < usersCount; i++)
            {
                User newUser = new User
                {
                    UserName = StringGenerator.RandomStringWithoutSpaces(6, 20),
                    Email = StringGenerator.RandomStringWithoutSpaces(6, 20) + "@newssystem.com",
                };

                this.userManager.Create(newUser, StringGenerator.RandomStringWithoutSpaces(6, 20));
                this.userManager.AddToRole(newUser.Id, role);
                context.SaveChanges();
            }
        }

        private void TokenNSImagesSeed()
        {
            if (!this.Data.Context.Tags.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    var tag = new Tag
                    {
                        Name = StringGenerator.RandomStringWithoutSpaces(5, 30),
                    };

                    this.Data.Tags.Add(tag);
                    //context.TokensNSImages.AddOrUpdate(newNSImageToken);
                }

                this.Data.SaveChanges();
                //context.SaveChanges();
            }
        }

        private void AlbumCategoriesSeed()
        {
            if (!this.Data.Context.Categories.Any())
            {
                // Add generated parent categories
                for (int i = 0; i < 5; i++)
                {
                    var category = new Category();
                    category.Title = StringGenerator.RandomStringWithSpaces(5, 40);
                    category.Description = StringGenerator.RandomStringWithSpaces(35, 400);
                    category.Summary = StringGenerator.RandomStringWithSpaces(20, 150);
                    category.ParentId = null;
                    category.IsRoot = true;

                    this.Data.Categories.Add(category);
                }

                this.Data.SaveChanges();

                var parents = this.Data.Categories.All()
                    .Where(c => c.IsRoot == true && c.ParentId == null)
                    .ToList();

                // Add generated child categories
                for (int i = 0; i < 30; i++)
                {
                    var category = new Category();
                    category.Title = StringGenerator.RandomStringWithSpaces(5, 40);
                    category.Description = StringGenerator.RandomStringWithSpaces(35, 400);
                    category.ParentId = parents[NumberGenerator.RandomNumber(0, parents.Count - 1)].Id;

                    this.Data.Categories.Add(category);
                }

                this.Data.SaveChanges();
            }
        }

        private void AlbumsSeed()
        {
            if (!this.Data.Context.Categories.Any())
            {
                return;
            }

            if (!this.Data.Context.Albums.Any())
            {
                var albumCategories = this.Data.Categories.All().Where(ac => ac.Parent != null).ToList();

                for (int i = 0; i < 50; i++)
                {
                    var newAlbum = new Album();
                    newAlbum.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    newAlbum.Description = StringGenerator.RandomStringWithSpaces(400, 1500);
                    newAlbum.Categories.Add(albumCategories[NumberGenerator.RandomNumber(0, albumCategories.Count - 1)]);
                    newAlbum.Summary = StringGenerator.RandomStringWithSpaces(70, 250);

                    this.Data.Albums.Add(newAlbum);
                    //context.Albums.AddOrUpdate(newAlbum);
                }

                this.Data.SaveChanges();
                //context.SaveChanges();
            }
        }

        private void ArticlesSeed()
        {
            if (!this.Data.Context.Articles.Any())
            {
                var users = Data.Users.All().ToList();
                for (int i = 0; i < 10; i++)
                {
                    var article = new Article();
                    article.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    article.Description = StringGenerator.RandomStringWithSpaces(400, 1500);
                    article.Summary = StringGenerator.RandomStringWithSpaces(70, 250);
                    article.AuthorId = users[NumberGenerator.RandomNumber(0, users.Count() - 1)].Id;

                    this.Data.Articles.Add(article);
                }

                this.Data.SaveChanges();
            }
        }
    }
}
