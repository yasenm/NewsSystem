using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using NewsSystem.Data.ViewModels.Users;
using System;

namespace NewsSystem.Data.ViewModels.Comments
{
    public class CommentBasicViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorName { get; set; }
    }
}
