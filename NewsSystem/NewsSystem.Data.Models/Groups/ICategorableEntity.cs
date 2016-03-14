namespace NewsSystem.Data.Models.Groups
{
    using System.Collections.Generic;

    public interface ICategorableEntity
    {
        ICollection<Category> Categories { get; set; }
    }
}
