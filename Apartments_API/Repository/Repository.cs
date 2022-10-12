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
            //_db.ApartmentNumbers.Include(u => u.Apartment).ToList(); // Apartment will be automatically populated
            //when we retrieve apartmentNumbers
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }



        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
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
            if (includeProperties != null)
            {
                //split include properties by the coma and and if there are any empty entries, remove them
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    q = q.Include(property);
                }
            }
            return await q.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> q = dbSet;
            if (filter != null)
            {
                q = q.Where(filter);
            }
            if (includeProperties != null)
            {
                //split include properties by the coma and and if there are any empty entries, remove them
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    q = q.Include(property);
                }
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
