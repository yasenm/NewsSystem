namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class Article : DeletableEntity
    {
        private ICollection<NSImage> headImages;

        public Article()
        {
            this.HeadImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<NSImage> HeadImages
        {
            get { return this.headImages; }
            set { this.headImages = value; }
        }
    }
}
