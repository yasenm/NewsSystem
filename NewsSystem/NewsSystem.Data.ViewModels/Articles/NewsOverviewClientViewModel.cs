namespace NewsSystem.Data.ViewModels.Articles
{
    using Infrastructure.Mapping;
    using Models;

    using NSImages;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Shared;

    public class NewsOverviewClientViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public NSImageOnlyIdViewModel CoverImage { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<string> CategoriesAssotiatied { get; set; }
        public StatsViewModel Stats { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, NewsOverviewClientViewModel>()
                .ForMember(m => m.CategoriesAssotiatied, opt => opt.MapFrom(
                            art => art.Categories.Select(c => c.Title).ToList()))
                .ForMember(m => m.CoverImage, opt => opt.MapFrom(
                            art => new NSImageOnlyIdViewModel {
                                Id = art.CoverImageId,
                                ImgTagClasses = "img-responsive" }))
                .ForMember(m => m.Stats,
                    opt => opt.MapFrom(art => new StatsViewModel
                    {
                        VisitorsCount = art.ViewsCount,
                        CommentsCount = art.Comments.Where(c => !c.IsDeleted).Count(),
                        CreatedOn = art.CreatedOn
                    }));
        }
    }
}
