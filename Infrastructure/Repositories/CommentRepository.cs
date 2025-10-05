using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CommentRepository : IBaseRepository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Comment Create(Comment obj)
        {
            var newObj = _context.Comments.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Comment Edit(Comment obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var modifiedObj = _context.Comments.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(Comment obj)
        {
            _context.Comments.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment?> GetById(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Comment> GetAll()
        {
            return _context.Comments.AsQueryable();
        }

        public IQueryable<Comment> GetLatestComments()
        {
            return _context.Comments.AsQueryable().OrderByDescending(c => c.DateCreated).Take(6);
        }

        public async Task<ICollection<Comment?>> GetCommentsByArticleIdAsync(int articleId)
        {
            return await _context.Comments.Where(c => c.ArticleId == articleId).ToListAsync();
        }
    }
}


