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
        private ICollection<Category> _categories;
        private ICollection<Tag> _tags;

        public Article()
        {
            this.Categories = new HashSet<Category>();
            this.Tags = new HashSet<Tag>();
        }

        [Key]
        public long Id { get; set; }

        public bool IsVideoNews { get; set; }

        public string VideoUrl { get; set; }

        public bool IsMain { get; set; }

        public bool IsTopMain { get; set; }

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

        public DateTime? PublicationDate { get; set; }

        public bool IsPublished { get; set; }

        public string PublishApprovedBy { get; set; }

        public bool IsQueuedForPublish { get; set; }

        public long CoverImageId { get; set; }

        public long? RelatedAlbumId { get; set; }

        public long? ThemeId { get; set; }

        public virtual Theme Theme { get; set; }
    }
}
