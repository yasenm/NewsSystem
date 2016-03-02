namespace NewsSystem.Data.Services.Contracts
{
    using System.Collections.Generic;

    public interface ITagsService
    {
        ICollection<string> GetAllTagsNames();
    }
}
