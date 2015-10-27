namespace NewsSystem.Data.ViewModels.Albums
{
    using AutoMapper;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using NewsSystem.Data.ViewModels.NSImages;

    public class AlbumGridViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? CoverImageId { get; set; }

        public NSImageGridViewModel NSImage { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, AlbumGridViewModel>()
                .ForMember(m => m.NSImage, opt => opt.MapFrom(src => Mapper.Map<NSImageGridViewModel>(src.CoverImage)));
        }
    }
}
