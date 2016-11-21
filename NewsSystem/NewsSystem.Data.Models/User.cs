namespace NewsSystem.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        private ICollection<Comment> _userComments;
        private ICollection<Article> _authoredNews;

        public User()
        {
            Comments = new HashSet<Comment>();
            AuthoredNews = new HashSet<Article>();
        }

        public virtual ICollection<Comment> Comments
        {
            get { return _userComments; }
            set { _userComments = value; }
        }

        public virtual ICollection<Article> AuthoredNews
        {
            get { return _authoredNews; }
            set { _authoredNews = value; }
        }
    }
}
