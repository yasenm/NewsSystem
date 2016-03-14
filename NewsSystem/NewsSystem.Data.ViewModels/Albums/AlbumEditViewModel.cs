namespace NewsSystem.Data.ViewModels.Albums
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using NewsSystem.Data.ViewModels.NSImages;
    using Common;

    public class AlbumEditViewModel : DescribableEntityViewModel, IMapFrom<Album>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public long AlbumCategoryId { get; set; }

        public long? CoverImageId { get; set; }

        public ICollection<string> Tags { get; set; }

        public ICollection<HttpPostedFileBase> AlbumPostedImages { get; set; }

        public ICollection<NSImageGridViewModel> AlbumImages { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, AlbumEditViewModel>()
                .ForMember(m => m.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
        }
    }
}
