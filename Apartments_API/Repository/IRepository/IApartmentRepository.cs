using Apartments_API.Models;
using System.Linq.Expressions;

namespace Apartment_API.Repository.IRepository
{
    public interface IApartmentRepository
    {
        Task<List<Apartment>> GetAll(Expression<Func<Apartment, bool>> filter = null);
        Task<Apartment> Get(Expression<Func<Apartment, bool>> filter = null, bool tracked=true);
        Task Create(Apartment entity);
        Task Remove(Apartment entity);
        Task Save();
    }
}
