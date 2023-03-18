using AutoMapper;
using Cars.API.Models.DTOs;
using WebMvcClient.ViewModels;

namespace WebMvcClient.AutomapperProfiles;

public class CarDTOMapperConfiguration : Profile
{
    public CarDTOMapperConfiguration()
    {
        CreateMap<CarDTO, CarViewModel>();
        CreateMap<CarMainInfoDTO, CarMainInfoViewModel>();
        CreateMap<CarMainInfoViewModel, CarMainInfoDTO>();
        CreateMap<CarViewModel, CarDTO>();
    }
}