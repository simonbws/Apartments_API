using Apartments_API.Models;
using System.Linq.Expressions;

namespace Apartment_API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
            int pageSize = 0, int pageNumber = 1); // nullable property because its possible you dont need this all time
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null); // dont need to be null
        //because we are retrieving only one condition
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
