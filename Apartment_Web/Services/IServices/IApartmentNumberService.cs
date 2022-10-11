using Apartment_Web.Models.DTO;

namespace Apartment_Web.Services.IServices
{
    public interface IApartmentNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ApartmentNumberCreateDTO dto);
        Task<T> UpdateAsync<T>(ApartmentNumberUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
