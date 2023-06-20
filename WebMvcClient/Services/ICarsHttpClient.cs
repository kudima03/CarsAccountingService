using Cars.API.Models.DTOs;

namespace WebMvcClient.Services;

public interface ICarsHttpClient
{
    Task<List<CarMainInfoDTO>> GetAllCarsAsync();

    Task<List<CarMainInfoDTO>> GetCarsRangeAsync(int skip, int take);

    Task<CarDTO> GetCarAsync(int carId);

    Task CreateCarAsync(CarDTO car);

    Task UpdateCarAsync(CarDTO car);

    Task DeleteCarAsync(int carId);
}