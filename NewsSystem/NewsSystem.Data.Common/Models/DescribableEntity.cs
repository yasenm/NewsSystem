namespace NewsSystem.Data.Common.Models
{
    public class DescribableEntity : DeletableEntity, IDescribableEntity
    {
        public string Description { get; set; }

        public string Summary { get; set; }

        public string Title { get; set; }
    }
}
