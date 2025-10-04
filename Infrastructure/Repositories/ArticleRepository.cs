using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;

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

        public void Delete(Article obj)
        {
            _context.Articles.Remove(obj);
            _context.SaveChangesAsync();
        }

        public async Task<Article?> GetById(int id)
        {
            return await _context.Articles.Include(a => a.Comments).Include(a => a.Writer).FirstOrDefaultAsync(a => a.Id == id);

        }

        public IQueryable<Article> GetAll()
        {
            return _context.Articles.AsQueryable();
        }

        public IQueryable<Article> GetMostView()
        {
            return _context.Articles.AsQueryable().OrderByDescending(a => a.ViewCount).Take(4);
        }

        public IQueryable<Article> GetRecents()
        {
            return _context.Articles.AsQueryable().OrderByDescending(a => a.DateCreated).Take(4);
        }

    }
}


