namespace NewsSystem.Data.ViewModels.Articles
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    using System;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
