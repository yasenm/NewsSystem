namespace NewsSystem.Data.Models
{
    using Contracts;
    using Groups;

    using NewsSystem.Data.Common.Models;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;

    public class Article : DescribableEntity, ITagableEntity, IPublishableEntity, ICategorableEntity
    {
        private ICollection<Category> categories;
        private ICollection<Tag> tags;
        private ICollection<NSImage> headImages;

        public Article()
        {
            this.Categories = new HashSet<Category>();
            this.Tags = new HashSet<Tag>();
            this.HeadImages = new HashSet<NSImage>();
        }

        [Key]
        public long Id { get; set; }

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

        public virtual ICollection<NSImage> HeadImages
        {
            get { return this.headImages; }
            set { this.headImages = value; }
        }

        public DateTime? PublicationDate { get; set; }

        public bool IsPublished { get; set; }

        public string PublishApprovedBy { get; set; }

        public bool IsQueuedForPublish { get; set; }
    }
}
