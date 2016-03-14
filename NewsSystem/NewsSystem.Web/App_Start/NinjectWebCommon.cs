[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NewsSystem.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NewsSystem.Web.App_Start.NinjectWebCommon), "Stop")]

namespace NewsSystem.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using NewsSystem.Data;
    using NewsSystem.Data.Services.Albums;
    using NewsSystem.Data.Services.Articles;
    using NewsSystem.Data.Services.Contracts;
    using NewsSystem.Data.Services.Contracts.Albums;
    using NewsSystem.Data.Services.Contracts.Category;
    using NewsSystem.Data.Services.Contracts.NSImages;
    using NewsSystem.Data.Services.Images;
    using NewsSystem.Data.UnitOfWork;
    using Data.Services.Tags;

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

            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<IAlbumService>().To<AlbumService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<ITagsService>().To<TagsService>();
            kernel.Bind<INSImageService>().To<NSImageService>();
        }        
    }
}
