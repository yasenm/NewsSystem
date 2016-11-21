using NewsSystem.Data.Common.Models;

namespace NewsSystem.Data.Models
{
    public class Comment : DeletableEntity
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}