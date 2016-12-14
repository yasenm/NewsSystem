using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsSystem.Data.Models
{
    public class VisitorIp
    {
        private ICollection<Article> _articles;

        public VisitorIp()
        {
            Articles = new HashSet<Article>();
        }

        public long Id { get; set; }
        [StringLength(39)]
        [Index(IsUnique = true)]
        public string IpAddress { get; set; }
        public DateTime LastVisit { get; set; }

        public virtual ICollection<Article> Articles
        {
            get { return _articles; }
            set { _articles = value; }
        }
    }
}
