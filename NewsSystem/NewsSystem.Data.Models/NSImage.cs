namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;
    using Groups;

    public class NSImage : DescribableEntity, ITagableEntity
    {
        private ICollection<Article> articles;
        private ICollection<Album> albums;
        private ICollection<Tag> tags;
        
        public NSImage()
        {
            this.Articles = new HashSet<Article>();
            this.Albums = new HashSet<Album>();
            this.Tags = new HashSet<Tag>();
        }

        [Key]
        public long Id { get; set; }

        public string ImageTags { get; set; }

        public byte[] ByteContent { get; set; }

        public string Extension { get; set; }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }
    }
}
