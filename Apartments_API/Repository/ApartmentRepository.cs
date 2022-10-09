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
        public async Task Create(Apartment entity)
        {
            await _db.Apartments.AddAsync(entity);
            await Save();
        }

       

        public async Task<Apartment> Get(Expression<Func<Apartment, bool>> filter = null, bool tracked = true)
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

        public async Task<List<Apartment>> GetAll(Expression<Func<Apartment, bool>> filter = null)
        {
            IQueryable<Apartment> q = _db.Apartments;
            if(filter!= null)
            {
                q = q.Where(filter);
            }
            return await q.ToListAsync();
            
        }

       

        public async Task Remove(Apartment entity)
        {
            _db.Apartments.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
