using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsSystem.Data.ViewModels.Comments
{
    public class CommentAddOrUpdateViewModel : IMapFrom<Comment>
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(500, MinimumLength = 20)]
        public string Content { get; set; }
        public long ArticleId { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 3)]
        public string AuthorName { get; set; }
    }
}
