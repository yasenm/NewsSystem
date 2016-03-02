namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class Album : DeletableEntity
    {
        private ICollection<Tag> tags;
        private ICollection<NSImage> nsImages;

        public Album()
        {
            this.Tags = new HashSet<Tag>();
            this.NSImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        [StringLength(200, MinimumLength = 4)]
        public string Name { get; set; }

        [StringLength(5000, MinimumLength = 4)]
        public string Text { get; set; }

        public long? CoverImageId { get; set; }

        public long AlbumCategoryId { get; set; }

        public virtual AlbumCategory AlbumCategory { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this.nsImages; }
            set { this.nsImages = value; }
        }
    }
}
