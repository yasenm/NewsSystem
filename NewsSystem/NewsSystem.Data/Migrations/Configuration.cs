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
        private UserManager<User> _userManager;
        private UserStore<User> _userStore;
        private INewsSystemData _data;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(NewsSystemDbContext context)
        {
            _userStore = new UserStore<User>(context);
            _userManager = new UserManager<User>(_userStore);
            _data = new NewsSystemData(context);

            SeedUserRoles(context);
            SeedStartUsers(context);
            ArticlesSeed();
            CommentsSeed();
            AlbumCategoriesSeed();
            AlbumsSeed();
            TokenNSImagesSeed();
            QuestionsSeed();
        }

        private void QuestionsSeed()
        {
            if (!this._data.Context.Questions.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    var question = new Question();
                    question.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    question.Description = StringGenerator.RandomStringWithSpaces(400, 1500);
                    question.Summary = StringGenerator.RandomStringWithSpaces(70, 250);

                    this._data.Questions.Add(question);
                    this._data.SaveChanges();

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

            this._data.Answers.Add(answer);
            this._data.SaveChanges();
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

            this._userManager.Create(admin, UsersConstants.MasterAdminPassword);
            this._userManager.AddToRole(admin.Id, UsersConstants.AdminRole);
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

                this._userManager.Create(newUser, StringGenerator.RandomStringWithoutSpaces(6, 20));
                this._userManager.AddToRole(newUser.Id, role);
                context.SaveChanges();
            }
        }

        private void TokenNSImagesSeed()
        {
            if (!this._data.Context.Tags.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    var tag = new Tag
                    {
                        Name = StringGenerator.RandomStringWithoutSpaces(5, 30),
                    };

                    this._data.Tags.Add(tag);
                    //context.TokensNSImages.AddOrUpdate(newNSImageToken);
                }

                this._data.SaveChanges();
                //context.SaveChanges();
            }
        }

        private void AlbumCategoriesSeed()
        {
            if (!this._data.Context.Categories.Any())
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

                    this._data.Categories.Add(category);
                }

                this._data.SaveChanges();

                var parents = this._data.Categories.All()
                    .Where(c => c.IsRoot == true && c.ParentId == null)
                    .ToList();

                // Add generated child categories
                for (int i = 0; i < 30; i++)
                {
                    var category = new Category();
                    category.Title = StringGenerator.RandomStringWithSpaces(5, 40);
                    category.Description = StringGenerator.RandomStringWithSpaces(35, 400);
                    category.ParentId = parents[NumberGenerator.RandomNumber(0, parents.Count - 1)].Id;

                    this._data.Categories.Add(category);
                }

                this._data.SaveChanges();
            }
        }

        private void AlbumsSeed()
        {
            if (!this._data.Context.Categories.Any())
            {
                return;
            }

            if (!this._data.Context.Albums.Any())
            {
                var albumCategories = this._data.Categories.All().Where(ac => ac.Parent != null).ToList();

                for (int i = 0; i < 50; i++)
                {
                    var newAlbum = new Album();
                    newAlbum.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    newAlbum.Description = StringGenerator.RandomStringWithSpaces(400, 1500);
                    newAlbum.Categories.Add(albumCategories[NumberGenerator.RandomNumber(0, albumCategories.Count - 1)]);
                    newAlbum.Summary = StringGenerator.RandomStringWithSpaces(70, 250);

                    this._data.Albums.Add(newAlbum);
                    //context.Albums.AddOrUpdate(newAlbum);
                }

                this._data.SaveChanges();
                //context.SaveChanges();
            }
        }

        private void ArticlesSeed()
        {
            if (!this._data.Context.Articles.Any())
            {
                var users = _data.Users.All().ToList();
                for (int i = 0; i < 10; i++)
                {
                    var article = new Article();
                    article.Title = StringGenerator.RandomStringWithoutSpaces(5, 40);
                    article.Description = StringGenerator.RandomStringWithSpaces(400, 1500);
                    article.Summary = StringGenerator.RandomStringWithSpaces(70, 250);
                    article.AuthorId = users[NumberGenerator.RandomNumber(0, users.Count - 1)].Id;

                    this._data.Articles.Add(article);
                }

                this._data.SaveChanges();
            }
        }

        private void CommentsSeed()
        {
            if (!this._data.Context.Comments.Any())
            {
                var users = _data.Users.All().ToList();
                var articles = _data.Articles.All().ToList();

                foreach (var news in articles)
                {
                    var randomNumberOfComments = NumberGenerator.RandomNumber(0, 6);
                    if (randomNumberOfComments > 1)
                    {
                        for (int i = 0; i < randomNumberOfComments; i++)
                        {
                            var newComment = new Comment
                            {
                                ArticleId = news.Id,
                                AuthorName = StringGenerator.RandomStringWithSpaces(3, 20),
                                Content = StringGenerator.RandomStringWithSpaces(20, 300)
                            };
                            _data.Comments.Add(newComment);
                        }
                        _data.SaveChanges();
                    }
                }
            }
        }
    }
}
