namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;
    using Groups;

    public class Article : DescribableEntity, ITagableEntity
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
