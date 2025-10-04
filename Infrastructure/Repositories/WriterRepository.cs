using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WriterRepository : IBaseRepository<Writer>, IWriterRepository
    {
        private readonly AppDbContext _context;

        public WriterRepository(AppDbContext context)
        {
            _context = context;
        }

        public Writer Create(Writer obj)
        {
            var newObj = _context.Writers.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Writer Edit(Writer obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var modifiedObj = _context.Writers.Update(obj);
            _context.SaveChangesAsync();
            return obj;
        }

        public void Delete(Writer obj)
        {
            _context.Writers.Remove(obj);
            _context.SaveChangesAsync();
        }

        public async Task<Writer?> GetById(int id)
        {
            return await _context.Writers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Writer> GetAll()
        {
            return _context.Writers.AsQueryable();
        }

        public async Task<ICollection<Article>> GetArticlesByWriterIdAsync(int id)
        {
            return await _context.Articles.Where(a => a.WriterId == id).ToListAsync();
        }
    }
}


