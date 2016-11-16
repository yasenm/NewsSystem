namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;

    using NewsSystem.Data.Common.Models;

    public class Question : DescribableEntity
    {
        private ICollection<Answer> _answers;

        public Question()
        {
            this._answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this._answers; }
            set { this._answers = value; }
        }
    }
}
