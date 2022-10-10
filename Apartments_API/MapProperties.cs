using Apartment_API.Models;
using Apartments_API.Models;
using Apartments_API.Models.DTO;
using AutoMapper;

namespace Apartment_API
{
    public class MapProperties : Profile
    {
        public MapProperties()
        {
            CreateMap<Apartment, ApartmentDTO>();
            CreateMap<ApartmentDTO, Apartment>();

            CreateMap<Apartment, ApartmentCreateDTO>().ReverseMap();
            CreateMap<Apartment, ApartmentUpdateDTO>().ReverseMap();

            CreateMap<ApartmentNumber, ApartmentNumberDTO>().ReverseMap();
            CreateMap<ApartmentNumber, ApartmentNumberCreateDTO>().ReverseMap();
            CreateMap<ApartmentNumber, ApartmentNumberUpdateDTO>().ReverseMap();

        }
    }
}
