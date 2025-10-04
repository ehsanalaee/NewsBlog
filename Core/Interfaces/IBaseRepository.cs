using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Create(T obj);
        T Edit(T obj);
        void Delete(T obj);
        IQueryable<T> GetAll();
        Task<T?> GetById(int id);
    }
}


