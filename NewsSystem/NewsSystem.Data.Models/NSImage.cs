﻿namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;
    using Groups;

    public class NSImage : DescribableEntity, ITagableEntity
    {
        private ICollection<Album> _albums;
        private ICollection<Tag> _tags;
        
        public NSImage()
        {
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
            get { return this._albums; }
            set { this._albums = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this._tags; }
            set { this._tags = value; }
        }
    }
}
