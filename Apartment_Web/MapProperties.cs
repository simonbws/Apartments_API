using Apartment_Web.Models.DTO;
using AutoMapper;

namespace Apartment_Web
{
    public class MapProperties : Profile
    {
        public MapProperties()
        {
            
            CreateMap<ApartmentDTO, ApartmentCreateDTO>().ReverseMap();
            CreateMap<ApartmentDTO, ApartmentUpdateDTO>().ReverseMap();

            CreateMap<ApartmentNumberDTO, ApartmentNumberCreateDTO>().ReverseMap();
            CreateMap<ApartmentNumberDTO, ApartmentNumberUpdateDTO>().ReverseMap();

        }
    }
}
