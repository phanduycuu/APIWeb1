using APIWeb1.Data;
using APIWeb1.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIWeb1.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IQueryable<T>> include = null,
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            IQueryable<T> query = dbSet;

            // Apply includes if provided
            if (include != null)
            {
                query = include(query);
            }

            // Apply filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply sorting if provided
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Apply pagination
            var skip = (pageNumber - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            // Execute the query and return the results
            return await query.ToListAsync();
        }
        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }


}
