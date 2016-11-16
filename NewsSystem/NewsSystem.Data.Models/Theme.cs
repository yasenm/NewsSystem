namespace NewsSystem.Data.Models
{
    using Contracts;
    using Groups;
    using NewsSystem.Data.Common.Models;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;

    public class Theme : DescribableEntity, ITagableEntity, ICategorableEntity
    {
        private ICollection<Article> _articles;
        private ICollection<Category> _categories;
        private ICollection<Tag> _tags;

        public Theme()
        {
            this.Articles = new HashSet<Article>();
            this.Categories = new HashSet<Category>();
            this.Tags = new HashSet<Tag>();
        }

        [Key]
        public long Id { get; set; }

        public virtual ICollection<Article> Articles
        {
            get { return this._articles; }
            set { this._articles = value; }
        }

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
    }
}
