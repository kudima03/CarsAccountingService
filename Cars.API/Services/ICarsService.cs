using Cars.API.Models.DTOs;

namespace Cars.API.Services;

public interface ICarsService : IDisposable
{
    Task<IEnumerable<CarMainInfoDTO>> GetAllAsync();
    Task<IEnumerable<CarDTO>> GetRangeAsync(int startIndex, int stopIndex);
    Task<CarDTO> GetByIdAsync(long id);
    Task<CarDTO> CreateAsync(CarDTO entity);
    Task<CarDTO> UpdateAsync(CarDTO entity);
    Task DeleteAsync(CarDTO entity);
}