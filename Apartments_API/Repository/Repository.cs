using Apartment_API.Database;
using Apartments_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Apartment_API.Repository.IRepository;

namespace Apartment_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }



        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            //here we got individual villa
            IQueryable<T> q = dbSet;
            if (!tracked)
            {
                q = q.AsNoTracking();
            }
            if (filter != null)
            {
                q = q.Where(filter);
            }
            return await q.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> q = dbSet;
            if (filter != null)
            {
                q = q.Where(filter);
            }
            return await q.ToListAsync();

        }



        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        
    }
}
