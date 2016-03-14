namespace NewsSystem.Data.Models.Groups
{
    using System.Collections.Generic;

    public interface ITagableEntity
    {
        ICollection<Tag> Tags { get; set; }
    }
}
