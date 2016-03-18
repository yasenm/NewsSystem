namespace NewsSystem.Data.Models.Contracts
{
    using System;

    public interface IPublishableEntity
    {
        DateTime? PublicationDate { get; set; }

        bool IsPublished { get; set; }

        string PublishApprovedBy { get; set; }
        
        bool IsQueuedForPublish { get; set; }
    }
}
