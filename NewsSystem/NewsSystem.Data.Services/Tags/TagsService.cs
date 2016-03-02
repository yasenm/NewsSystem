namespace NewsSystem.Data.Services.Tags
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NewsSystem.Data.Services.Contracts;
    using UnitOfWork;

    public class TagsService : IDataService, ITagsService
    {
        public INewsSystemData Data { get; set; }

        public TagsService(INewsSystemData data)
        {
            this.Data = data;
        }

        public ICollection<string> GetAllTagsNames()
        {
            var result = this.Data.Tags.All().Select(t => t.Name).ToList();
            return result;
        }
    }
}
