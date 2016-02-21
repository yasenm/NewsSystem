namespace NewsSystem.Data.ViewModels.Albums
{
    using System.Linq;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;
    using NewsSystem.Data.ViewModels.NSImages;
    using System;

    public class AlbumEditViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public long Id { get; set; }

        [StringLength(200, MinimumLength = 4, ErrorMessage = "You must use more than 4 and less than 200 characters")]
        public string Name { get; set; }
        
        [StringLength(5000, MinimumLength = 4, ErrorMessage = "You must use more than 4 and less than 5000 characters")]
        public string Text { get; set; }

        public long AlbumCategoryId { get; set; }

        public long? CoverImageId { get; set; }

        public ICollection<string> Tokens { get; set; }

        public ICollection<HttpPostedFileBase> AlbumPostedImages { get; set; }

        public ICollection<NSImageGridViewModel> AlbumImages { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, AlbumEditViewModel>()
                .ForMember(m => m.Tokens, opt => opt.MapFrom(src => src.AlbumTokens.Select(t => t.Name)));
        }
    }
}
