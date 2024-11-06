using System.Linq.Expressions;

namespace APIWeb1.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /* ADMIN */
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        //Task<IEnumerable<T>> GetAll_WSETAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        //Task<IEnumerable<T>> GetListTrueAsync(Expression<Func<T, bool>> filter);
    }
}
