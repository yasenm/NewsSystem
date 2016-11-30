namespace NewsSystem.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        private ICollection<Article> _authoredNews;

        public User()
        {
            AuthoredNews = new HashSet<Article>();
        }

        public virtual ICollection<Article> AuthoredNews
        {
            get { return _authoredNews; }
            set { _authoredNews = value; }
        }
    }
}
