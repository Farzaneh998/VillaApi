using System.Linq.Expressions;

namespace Rent.Reposotory.IReposotory
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null,bool tracked=true);
        Task<List<T>> GetAllAsync(Expression< Func<T, bool>> filter= null);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
