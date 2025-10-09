using Core.Entities;


namespace Core.Interfaces
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Task<List<Article>> GetMostView();
        Task<List<Article>> GetRecents();
        Task<List<Article>> GetRelated(Article article);

        void RaisedView(Article article);
    }
}


