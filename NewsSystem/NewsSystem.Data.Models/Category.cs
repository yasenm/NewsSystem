namespace NewsSystem.Data.Models
{
    using NewsSystem.Data.Common.Models;

    using System.Collections.Generic;

    public class Category : DescribableEntity
    {
        private ICollection<Category> children;
        private ICollection<Album> albums;
        private ICollection<Article> articles;
        private ICollection<NSImage> nsImages;

        public Category()
        {
            this.Children = new HashSet<Category>();
            this.Albums = new HashSet<Album>();
            this.Articles = new HashSet<Article>();
            this.nsImages = new HashSet<NSImage>();
        }

        public long Id { get; set; }

        public bool IsRoot { get; set; }

        public long? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children
        {
            get { return this.children; }
            set { this.children = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this.nsImages; }
            set { this.nsImages = value; }
        }
    }
}
