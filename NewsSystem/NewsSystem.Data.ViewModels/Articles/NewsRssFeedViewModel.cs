using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using System;

namespace NewsSystem.Data.ViewModels.Articles
{
    public class NewsRssFeedViewModel : IMapFrom<Article>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public long CoverImageId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}