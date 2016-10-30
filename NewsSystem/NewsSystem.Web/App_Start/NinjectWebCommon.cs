[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NewsSystem.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NewsSystem.Web.App_Start.NinjectWebCommon), "Stop")]

namespace NewsSystem.Web.App_Start
{
    using Data.Services.Contracts.Surveys;
    using Data.Services.Surveys;
    using Data.Services.Themes;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Data;
    using Data.Services.Albums;
    using Data.Services.Articles;
    using Data.Services.Contracts;
    using Data.Services.Contracts.Albums;
    using Data.Services.Contracts.Category;
    using Data.Services.Contracts.NSImages;
    using Data.Services.Images;
    using Data.Services.Tags;
    using Data.Services.Category;
    using Data.UnitOfWork;

    using Ninject;
    using Ninject.Web.Common;

    using System;
    using System.Web;

    using Helpers;
    using Helpers.Contracts;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<INewsSystemDbContext>().To<NewsSystemDbContext>();
            kernel.Bind<INewsSystemData>().To<NewsSystemData>();

            // Admin services
            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<IAlbumService>().To<AlbumService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<ITagsService>().To<TagsService>();
            kernel.Bind<INSImageService>().To<NSImageService>();
            kernel.Bind<IThemeService>().To<ThemeService>();
            kernel.Bind<IQuestionsService>().To<QuestionsService>();
            kernel.Bind<IAnswersService>().To<AnswersService>();

            kernel.Bind<IGridMvcHelper>().To<GridMvcHelper>();

            // Client side services
            kernel.Bind<IArticleClientService>().To<ArticleClientService>();
            kernel.Bind<ICategoryClientService>().To<CategoryClientService>();
        }
    }
}
