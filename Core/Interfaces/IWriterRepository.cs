using Core.Entities;

namespace Core.Interfaces
{
    public interface IWriterRepository : IBaseRepository<Writer>
    {
        Task<ICollection<Article>> GetArticlesByWriterIdAsync(int id);
    }
}

