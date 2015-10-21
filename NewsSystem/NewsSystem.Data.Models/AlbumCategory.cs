namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class AlbumCategory : DeletableEntity
    {
        private ICollection<AlbumCategory> children;
        private ICollection<Album> albums;

        public AlbumCategory()
        {
            this.Children = new HashSet<AlbumCategory>();
            this.Albums = new HashSet<Album>();
        }

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public long? ParentId { get; set; }

        public virtual AlbumCategory Parent { get; set; }

        public virtual ICollection<AlbumCategory> Children
        {
            get { return this.children; }
            set { this.children = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }
    }
}
