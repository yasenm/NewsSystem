namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class NSImage : DeletableEntity
    {
        private ICollection<Album> albums;
        private ICollection<TokenNSImage> tokenNSImages;
        
        public NSImage()
        {
            this.Albums = new HashSet<Album>();
            this.TokensNSImages = new HashSet<TokenNSImage>();
        }

        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string ImageTags { get; set; }
        
        public string Text { get; set; }

        public byte[] ByteContent { get; set; }

        public string Extension { get; set; }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public virtual ICollection<TokenNSImage> TokensNSImages
        {
            get { return this.tokenNSImages; }
            set { this.tokenNSImages = value; }
        }
    }
}
