using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class ArticleRepository : IBaseRepository<Article>, IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public Article Create(Article obj)
        {
            var newObj = _context.Articles.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Article Edit(Article obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var modifiedObj = _context.Articles.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(Article obj)
        {
            _context.Articles.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Article?> GetById(int id)
        {
            return await _context.Articles.Include(a => a.Comments).Include(a => a.Writer).Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);

        }

        public IQueryable<Article> GetAll()
        {
            return _context.Articles
                .Include(a => a.Category)
                .Include(a => a.Writer)
                .AsQueryable();
        }

        public async Task<List<Article>> GetMostView()
        {
            return await _context.Articles.OrderByDescending(a => a.ViewCount).Take(4).Include(a => a.Writer).Include(a => a.Category).ToListAsync();
        }

        public async Task<List<Article>> GetRecents()
        {
            return await _context.Articles.OrderByDescending(a => a.DateCreated).Take(4).Include(a => a.Writer).Include(a => a.Category).ToListAsync();
        }

        public async Task<List<Article>> GetRelated(Article article)
        {
            return await _context.Articles.Where(a => a.CategoryId == article.CategoryId).OrderByDescending(a => a.DateCreated).Take(5).Include(a => a.Writer).Include(a => a.Category).ToListAsync();
        }

        public void RaisedView(Article article)
        {
            if (article != null)
            {
                article.ViewCount++;
                _context.SaveChanges();
            }
        }
    }
}


