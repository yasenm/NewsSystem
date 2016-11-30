using NewsSystem.Data.Common.Models;

namespace NewsSystem.Data.Models
{
    public class Comment : DeletableEntity
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long AvatarId { get; set; }
        public string AvatarUrl { get; set; }
        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}