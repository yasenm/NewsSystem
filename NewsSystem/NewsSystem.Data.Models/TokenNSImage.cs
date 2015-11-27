namespace NewsSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class TokenNSImage : DeletableEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
