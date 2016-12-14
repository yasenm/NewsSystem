namespace NewsSystem.Data.ViewModels.Albums
{
    using AutoMapper;

    using Infrastructure.Mapping;
    using Models;

    using System.Collections.Generic;
    using System.Linq;

    public class AlbumClientMinViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public long AlbumCategoryId { get; set; }

        public long? CoverImageId { get; set; }

		public string Summary { get; set; }

        public ICollection<long> AlbumImagesIds { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, AlbumClientMinViewModel>()
                .ForMember(m => m.AlbumImagesIds, opt => opt.MapFrom(src => src.NSImages.Select(nsi => nsi.Id)));
        }
    }
}
