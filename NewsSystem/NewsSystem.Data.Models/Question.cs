namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;

    using NewsSystem.Data.Common.Models;

    public class Question : DeletableEntity
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}
