namespace NewsSystem.Data.Services.Themes
{
    using Contracts;
    using ViewModels.Themes;
    using UnitOfWork;

    using System;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    public class ThemeService : IDataService, IThemeService
    {
        public INewsSystemData Data { get; set; }

        public ThemeService(INewsSystemData data)
        {
            this.Data = data;
        }

        public IQueryable<ThemeBasicViewModel> GetAll()
        {
            var result = this.Data.Themes.All()
                .Project()
                .To<ThemeBasicViewModel>();

            return result;
        }
    }
}
