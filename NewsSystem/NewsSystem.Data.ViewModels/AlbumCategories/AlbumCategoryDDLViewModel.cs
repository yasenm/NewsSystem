namespace NewsSystem.Data.ViewModels.AlbumCategories
{
    using AutoMapper;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class AlbumCategoryDDLViewModel : IMapFrom<AlbumCategory>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsParent { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AlbumCategory, AlbumCategoryDDLViewModel>()
               .ForMember(m => m.IsParent, opt => opt.MapFrom(s => s.ParentId == null));
        }
    }
}
