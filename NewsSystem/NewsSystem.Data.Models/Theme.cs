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
        private ICollection<Article> articles;
        private ICollection<Category> categories;
        private ICollection<Tag> tags;

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
            get { return this.articles; }
            set { this.articles = value; }
        }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
