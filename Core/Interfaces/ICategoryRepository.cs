using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<ICollection<Article>> GetArticlesByCategoryIdAsync(int categoryId);
    }
}


