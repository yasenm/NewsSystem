using NewsSystem.Data.Common.Models;
using System.Collections.Generic;

namespace NewsSystem.Data.Models
{
    public class Comment : DeletableEntity
    {
        private ICollection<Vote> _votes;

        public Comment()
        {
            Votes = new HashSet<Vote>();
        }

        public long Id { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Vote> Votes
        {
            get { return _votes; }
            set { _votes = value; }
        }
        public string AuthorName { get; set; }
        public long AvatarId { get; set; }
        public string AvatarUrl { get; set; }
        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}