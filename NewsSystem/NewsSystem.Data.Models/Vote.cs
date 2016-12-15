namespace NewsSystem.Data.Models
{
    public class Vote
    {
        public long Id { get; set; }
        public bool IsPositive { get; set; }
        public long VisitorIpId { get; set; }
        public virtual VisitorIp VisitorIp { get; set; }
        public long CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}