namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class TokenNSImage : DeletableEntity
    {
        private ICollection<NSImage> nsImages;

        public TokenNSImage()
        {
            this.NSImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this.nsImages; }
            set { this.nsImages = value; }
        }
    }
}
