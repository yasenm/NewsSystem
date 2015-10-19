namespace NewsSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class Article : DeletableEntity
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
