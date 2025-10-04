using Core.Entities;


namespace Core.Interfaces
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        IQueryable<Article> GetMostView();
        IQueryable<Article> GetRecents();
    }
}


