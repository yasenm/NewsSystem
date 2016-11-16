namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;
    using Groups;

    public class Album : DescribableEntity, ITagableEntity, ICategorableEntity
    {
        private ICollection<Category> _categories;
        private ICollection<Tag> _tags;
        private ICollection<NSImage> _nsImages;

        public Album()
        {
            this.Categories = new HashSet<Category>();
            this.Tags = new HashSet<Tag>();
            this.NSImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        public long? CoverImageId { get; set; }
        
        public virtual ICollection<Category> Categories
        {
            get { return this._categories; }
            set { this._categories = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this._tags; }
            set { this._tags = value; }
        }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this._nsImages; }
            set { this._nsImages = value; }
        }
    }
}
