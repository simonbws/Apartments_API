using Apartment_API.Models;
using Apartments_API.Models;
using System.Linq.Expressions;

namespace Apartment_API.Repository.IRepository
{
    public interface IApartmentNumberRepository : IRepository<ApartmentNumber>
    {
        
        Task<ApartmentNumber> UpdateAsync(ApartmentNumber entity);
       
    }
}
