namespace NewsSystem.Data.ViewModels.Articles
{
    using AutoMapper;

    using Infrastructure.Mapping;
    using Models;
    using NSImages;

    using System;

    public class NewsEditorsChoiceOverviewClientViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public NSImageOnlyIdViewModel CoverImage { get; set; }
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, NewsEditorsChoiceOverviewClientViewModel>()
                .ForMember(m => m.CoverImage, opt => opt.MapFrom(art => new NSImageOnlyIdViewModel { Id = art.CoverImageId }));
        }
    }
}
