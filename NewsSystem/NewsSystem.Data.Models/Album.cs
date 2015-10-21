namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class Album : DeletableEntity
    {
        private ICollection<NSImage> nsImages;

        public Album()
        {
            this.NSImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        [StringLength(200, MinimumLength = 4)]
        public string Name { get; set; }

        [StringLength(5000, MinimumLength = 4)]
        public string Text { get; set; }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this.nsImages; }
            set { this.nsImages = value; }
        }
    }
}
