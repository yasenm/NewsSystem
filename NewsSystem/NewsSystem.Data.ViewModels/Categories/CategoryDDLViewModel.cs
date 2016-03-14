namespace NewsSystem.Data.ViewModels.Categories
{
    using AutoMapper;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class CategoryDDLViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public bool IsParent { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryDDLViewModel>()
               .ForMember(m => m.IsParent, opt => opt.MapFrom(s => s.ParentId == null));
        }
    }
}
