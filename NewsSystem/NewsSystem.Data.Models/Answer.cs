namespace NewsSystem.Data.Models
{
    using NewsSystem.Data.Common.Models;

    public class Answer : DeletableEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool ChosenCount { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
