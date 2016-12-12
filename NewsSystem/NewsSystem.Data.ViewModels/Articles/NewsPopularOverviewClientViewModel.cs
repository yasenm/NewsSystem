namespace NewsSystem.Data.ViewModels.Articles
{
    using Infrastructure.Mapping;
    using Models;

    using System;

    public class NewsPopularOverviewClientViewModel : IMapFrom<Article>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool IsTopMain { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
