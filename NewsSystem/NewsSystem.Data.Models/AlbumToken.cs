namespace NewsSystem.Data.Models
{
    using Common.Models;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AlbumToken : DeletableEntity
    {
        private ICollection<Album> albums;

        public AlbumToken()
        {
            this.Albums = new HashSet<Album>();
        }

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }
    }
}
