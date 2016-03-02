namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class Article : DeletableEntity
    {
        private ICollection<Tag> tags;
        private ICollection<NSImage> headImages;

        public Article()
        {
            this.Tags = new HashSet<Tag>();
            this.HeadImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<NSImage> HeadImages
        {
            get { return this.headImages; }
            set { this.headImages = value; }
        }
    }
}
