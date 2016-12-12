using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using System;

namespace NewsSystem.Data.ViewModels.Comments
{
    public class CommentBasicViewModel : IMapFrom<Comment>
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorName { get; set; }
        //public long AvatarId { get; set; }
        //public string AvatarUrl { get; set; }
    }
}
