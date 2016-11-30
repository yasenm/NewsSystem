using AutoMapper;
using AutoMapper.QueryableExtensions;
using NewsSystem.Data.Models;
using NewsSystem.Data.Services.Contracts.Comments;
using NewsSystem.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NewsSystem.Data.Services.Services.Comments
{
    public class CommentsClientService : ICommentsClientService
    {
        private INewsSystemData _data;

        public CommentsClientService(INewsSystemData data)
        {
            _data = data;
        }

        public bool AddOrUpdate<T>(T model)
        {
            try
            {
                var newComent = Mapper.Map<Comment>(model);
                _data.Comments.Add(newComent);
                _data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                _data.Comments.Delete(id);
                _data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;                
            }
        }

        public IEnumerable<T> GetByNewsId<T>(long newsId)
        {
            var result = GetCommentsByExpression<T>(c => c.ArticleId == newsId).ToList();
            return result;
        }

        public IEnumerable<T> GetByUser<T>(string username)
        {
            var result = GetCommentsByExpression<T>(c => c.AuthorName == username);
            return result;
        }

        private IQueryable<T> GetCommentsByExpression<T>(Expression<Func<Comment, bool>> func)
        {
            var result = _data.Comments.All()
                .Where(func)
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<T>();

            return result;
        }
    }
}
