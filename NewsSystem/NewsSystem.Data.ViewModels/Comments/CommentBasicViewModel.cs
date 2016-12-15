using NewsSystem.Data.Infrastructure.Mapping;
using NewsSystem.Data.Models;
using System;
using AutoMapper;
using System.Linq;
using System.Web;

namespace NewsSystem.Data.ViewModels.Comments
{
    public class CommentBasicViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorName { get; set; }
        public int Votes { get; set; }
        public int TotalVotesCount { get; set; }
        public bool UserVotedForThis { get; set; }
        public int PositiveVotes { get; set; }
        public int NegativeVotes
        {
            get
            {
                return TotalVotesCount - PositiveVotes;
            }
        }
        private decimal PercentsForVote
        {
            get
            {
                if (TotalVotesCount == 0)
                {
                    return 0;
                }
                return 100 / TotalVotesCount;
            }
        }
        public int PositiveVotesPercent
        {
            get
            {
                if (TotalVotesCount == 0)
                {
                    return 0;
                }
                return (int)(PercentsForVote * PositiveVotes);
            }
        }
        public int NegativeVotesPercent
        {
            get
            {
                if (TotalVotesCount == 0)
                {
                    return 0;
                }
                return (int)(PercentsForVote * NegativeVotes);
            }
        }

        //public long AvatarId { get; set; }
        //public string AvatarUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentBasicViewModel>()
                .ForMember(m => m.Votes, opt => opt.MapFrom(
                    c => c.Votes.Where(v => v.IsPositive).Count() - c.Votes.Where(v => v.IsPositive == false).Count()))
                .ForMember(m => m.PositiveVotes, opt => opt.MapFrom(
                    c => c.Votes.Where(v => v.IsPositive).Count()))
                .ForMember(m => m.TotalVotesCount, opt => opt.MapFrom(
                    c => c.Votes.Count()))
                .ForMember(m => m.UserVotedForThis, opt => opt.MapFrom(
                    c => c.Votes.FirstOrDefault(v => v.VisitorIp.IpAddress == HttpContext.Current.Request.UserHostAddress) != null));
        }
    }
}