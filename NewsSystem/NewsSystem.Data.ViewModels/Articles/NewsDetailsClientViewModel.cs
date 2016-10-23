using AutoMapper;

using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using NewsSystem.Data.ViewModels.NSImages;

using System;

namespace NewsSystem.Data.ViewModels.Articles
{
    public class NewsDetailsClientViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public long CoverImageId { get; set; }
        public NSImageOnlyIdViewModel CoverImage { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? RelatedAlbumId { get; set; }

        // Need map config for tags and categories

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, NewsDetailsClientViewModel>()
                .ForMember(m => m.Description,
                    opt => opt.MapFrom(art => art.Description))
                .ForMember(m => m.CoverImage, 
                    opt => opt.MapFrom(art => new NSImageOnlyIdViewModel { Id = art.CoverImageId, ImgTagClasses = "img-responsive" }));
        }
    }
}
