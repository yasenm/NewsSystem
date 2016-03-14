namespace NewsSystem.Data.ViewModels.Articles
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
