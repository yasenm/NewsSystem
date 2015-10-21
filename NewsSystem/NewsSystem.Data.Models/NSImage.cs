namespace NewsSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using NewsSystem.Data.Common.Models;

    public class NSImage : DeletableEntity
    {
        [Key]
        public long Id { get; set; }

        public string ImageTags { get; set; }

        public string Text { get; set; }

        public byte[] ByteContent { get; set; }

        public string Extension { get; set; }

        public long AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
