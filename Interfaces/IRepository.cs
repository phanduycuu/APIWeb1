using System.Linq.Expressions;

namespace APIWeb1.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /* ADMIN */
        //Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        //Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        //Task AddAsync(T entity);
        //Task RemoveAsync(T entity);
        //Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IQueryable<T>> include = null,
            int pageNumber = 1,
            int pageSize = 10
        );
        Task<T> CreateAsync(T entity);

        //Task<IEnumerable<T>> GetAll_WSETAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        //Task<IEnumerable<T>> GetListTrueAsync(Expression<Func<T, bool>> filter);
    }
}
