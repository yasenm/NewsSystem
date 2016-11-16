namespace NewsSystem.Data.Models
{
    using NewsSystem.Data.Common.Models;

    using System.Collections.Generic;

    public class Category : DescribableEntity
    {
        private ICollection<Category> _children;
        private ICollection<Album> _albums;
        private ICollection<Article> _articles;
        private ICollection<NSImage> _nsImages;

        public Category()
        {
            Children = new HashSet<Category>();
            Albums = new HashSet<Album>();
            Articles = new HashSet<Article>();
            NSImages = new HashSet<NSImage>();
        }

        public long Id { get; set; }

        public bool IsRoot { get; set; }

        public long? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children
        {
            get { return this._children; }
            set { this._children = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this._albums; }
            set { this._albums = value; }
        }

        public virtual ICollection<Article> Articles
        {
            get { return this._articles; }
            set { this._articles = value; }
        }

        public virtual ICollection<NSImage> NSImages
        {
            get { return this._nsImages; }
            set { this._nsImages = value; }
        }
    }
}
