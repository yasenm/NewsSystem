namespace NewsSystem.Data.Models
{
    using Common.Models;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag : DeletableEntity
    {
        private ICollection<Article> articles;
        private ICollection<Album> albums;
        private ICollection<NSImage> nsImages;

        public Tag()
        {
            this.Articles = new HashSet<Article>();
            this.Albums = new HashSet<Album>();
            this.NSImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this.nsImages; }
            set { this.nsImages = value; }
        }
    }
}
