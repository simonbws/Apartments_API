using Apartment_API.Database;
using Apartment_API.Models;
using Apartment_API.Repository.IRepository;
using Apartments_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Apartment_API.Repository
{
    public class ApartmentNumberRepository : Repository<ApartmentNumber>, IApartmentNumberRepository
    {
        private readonly AppDbContext _db;

        public ApartmentNumberRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
      
        public async Task<ApartmentNumber> UpdateAsync(ApartmentNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.ApartmentNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
