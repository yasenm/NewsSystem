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
        public INewsSystemData _data { get; set; }

        public ThemeService(INewsSystemData data)
        {
            this._data = data;
        }

        public IQueryable<ThemeBasicViewModel> GetAll()
        {
            var result = this._data.Themes.All()
                .Project()
                .To<ThemeBasicViewModel>();

            return result;
        }
    }
}
