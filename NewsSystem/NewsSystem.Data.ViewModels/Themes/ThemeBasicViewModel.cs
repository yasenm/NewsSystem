namespace NewsSystem.Data.ViewModels.Themes
{
    using Common;
    using Infrastructure.Mapping;
    using Models;

    public class ThemeBasicViewModel : DescribableEntityViewModel, IMapFrom<Theme> 
    {
        public long Id { get; set; }
    }
}
