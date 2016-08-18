namespace NewsSystem.Data.Services.Contracts
{
    using ViewModels.Themes;

    using System.Linq;

    public interface IThemeService
    {
        IQueryable<ThemeBasicViewModel> GetAll();
    }
}
