namespace NewsSystem.Data.Models
{
    using NewsSystem.Data.Common.Models;

    public class Answer : DescribableEntity
    {
        public int Id { get; set; }

        public bool ChosenCount { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
