using Core.Entities;
using Core.Interfaces;

namespace Core.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        IQueryable<Comment> GetLatestComments();
        Task<ICollection<Comment?>> GetCommentsByArticleIdAsync(int articleId);
    }
}


