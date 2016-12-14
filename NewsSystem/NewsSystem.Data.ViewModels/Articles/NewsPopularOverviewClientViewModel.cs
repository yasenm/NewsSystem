namespace NewsSystem.Data.ViewModels.Articles
{
    using AutoMapper;
    using Infrastructure.Mapping;
    using Models;
    using Shared;
    using System;
    using System.Linq;

    public class NewsPopularOverviewClientViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool IsTopMain { get; set; }
        public DateTime CreatedOn { get; set; }
        public StatsViewModel Stats { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, NewsPopularOverviewClientViewModel>()
                .ForMember(m => m.Stats,
                    opt => opt.MapFrom(art => new StatsViewModel
                    {
                        VisitorsCount = art.VisitorsIps.Count,
                        CommentsCount = art.Comments.Where(c => !c.IsDeleted).Count(),
                        CreatedOn = null,
                    }));
        }
    }
}
