using AutoMapper;
using Cars.API.Models.DTOs;

namespace Cars.API.Models.AutomapperProfiles;

public class CarMapperConfiguration : Profile
{
    public CarMapperConfiguration()
    {
        CreateMap<Car, CarDTO>();
        CreateMap<Car, CarMainInfoDTO>();
        CreateMap<CarMainInfoDTO, Car>();
        CreateMap<CarDTO, Car>();
    }
}