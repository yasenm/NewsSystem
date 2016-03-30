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

        public Article()
        {
            this.Categories = new HashSet<Category>();
            this.Tags = new HashSet<Tag>();
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

        public DateTime? PublicationDate { get; set; }

        public bool IsPublished { get; set; }

        public string PublishApprovedBy { get; set; }

        public bool IsQueuedForPublish { get; set; }

        public long CoverImageId { get; set; }

        public long? RelatedAlbumId { get; set; }
    }
}
