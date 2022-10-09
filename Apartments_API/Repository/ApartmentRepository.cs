using Apartment_API.Database;
using Apartment_API.Repository.IRepository;
using Apartments_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Apartment_API.Repository
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _db;

        public ApartmentRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Apartment entity)
        {
            await _db.Apartments.AddAsync(entity);
            await SaveAsync();
        }

       

        public async Task<Apartment> GetAsync(Expression<Func<Apartment, bool>> filter = null, bool tracked = true)
        {
            //here we got individual villa
            IQueryable<Apartment> q = _db.Apartments;
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

        public async Task<List<Apartment>> GetAllAsync(Expression<Func<Apartment, bool>> filter = null)
        {
            IQueryable<Apartment> q = _db.Apartments;
            if(filter!= null)
            {
                q = q.Where(filter);
            }
            return await q.ToListAsync();
            
        }

       

        public async Task RemoveAsync(Apartment entity)
        {
            _db.Apartments.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Apartment entity)
        {
            _db.Apartments.Update(entity);
            await SaveAsync();
        }
    }
}
