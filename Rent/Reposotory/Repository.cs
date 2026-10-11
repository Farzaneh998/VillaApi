using Microsoft.EntityFrameworkCore;
using Rent.Data;
using Rent.Reposotory.IReposotory;
using System.Linq.Expressions;

namespace Rent.Reposotory
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
            this.dbset = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
           dbset.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query.Where(filter);    
            }
            return await query.ToListAsync();

        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query= query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
