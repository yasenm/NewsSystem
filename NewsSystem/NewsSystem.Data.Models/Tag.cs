namespace NewsSystem.Data.Models
{
    using Common.Models;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag : DeletableEntity
    {
        private ICollection<Article> _articles;
        private ICollection<Album> _albums;
        private ICollection<NSImage> _nsImages;

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
            get { return this._articles; }
            set { this._articles = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this._albums; }
            set { this._albums = value; }
        }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this._nsImages; }
            set { this._nsImages = value; }
        }
    }
}
