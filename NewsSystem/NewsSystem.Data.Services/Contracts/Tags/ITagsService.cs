namespace NewsSystem.Data.Services.Contracts
{
    using Models;
    using Models.Groups;

    using System.Collections.Generic;

    public interface ITagsService
    {
        ICollection<string> GetAllTagsNames();

        bool SaveTagsToTagableEntity(ITagableEntity tagableEntity, ICollection<string> choosenTags);

        ICollection<Tag> GetTagsByTitle(ICollection<string> tagNames);
    }
}
