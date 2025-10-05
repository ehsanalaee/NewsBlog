using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : IBaseRepository<Category>, ICategoryRepository
    {
        private readonly Context.AppDbContext _context;

        public CategoryRepository(Context.AppDbContext context)
        {
            _context = context;
        }

        public Category Create(Category obj)
        {
            _context.Categories.AddAsync(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public Category Edit(Category obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var modifiedObj = _context.Categories.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(Category obj)
        {
            _context.Categories.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories.AsQueryable();
        }

        public async Task<ICollection<Article>> GetArticlesByCategoryIdAsync(int categoryId)
        {
            return await _context.Articles
                .Where(a => a.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}


