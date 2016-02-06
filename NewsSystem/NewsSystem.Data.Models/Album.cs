namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class Album : DeletableEntity
    {
        private ICollection<AlbumToken> aTokens;
        private ICollection<NSImage> nsImages;

        public Album()
        {
            this.AlbumTokens = new HashSet<AlbumToken>();
            this.NSImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        [StringLength(200, MinimumLength = 4)]
        public string Name { get; set; }

        [StringLength(5000, MinimumLength = 4)]
        public string Text { get; set; }

        public long? CoverImageId { get; set; }

        public virtual NSImage CoverImage { get; set; }

        public long AlbumCategoryId { get; set; }

        public virtual AlbumCategory AlbumCategory { get; set; }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this.nsImages; }
            set { this.nsImages = value; }
        }

        public virtual ICollection<AlbumToken> AlbumTokens
        {
            get { return this.aTokens; }
            set { this.aTokens = value; }
        }
    }
}
