using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cars.API.Data.Interfaces;
using Cars.API.Models;
using Cars.API.Models.DTOs;
using FluentValidation;

namespace Cars.API.Services;

public class CarsService : ICarsService
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Car> _repository;
    private readonly IValidator<CarDTO> _validator;

    private bool _disposed;

    public CarsService(IAsyncRepository<Car> repository, IMapper mapper, IValidator<CarDTO> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<CarMainInfoDTO>> GetAllAsync()
    {
        var carsQuery = await _repository.GetAllAsync();
        return carsQuery.ProjectTo<CarMainInfoDTO>(_mapper.ConfigurationProvider).AsEnumerable();
    }

    public async Task<IEnumerable<CarDTO>> GetRangeAsync(int startIndex, int stopIndex)
    {
        if (stopIndex == startIndex) return new List<CarDTO>();
        var orderByIdDescending = false;
        if (startIndex > stopIndex)
        {
            (startIndex, stopIndex) = (stopIndex, startIndex);
            orderByIdDescending = true;
        }

        var carsQuery = await _repository.GetRangeAsync(startIndex, stopIndex - startIndex);
        if (orderByIdDescending) carsQuery = carsQuery.OrderByDescending(x => x.Id);
        return carsQuery.ProjectTo<CarDTO>(_mapper.ConfigurationProvider).AsEnumerable();
    }

    public async Task<CarDTO> GetByIdAsync(long id)
    {
        var car = await _repository.GetByIdAsync(id);
        return _mapper.Map<CarDTO>(car);
    }

    public async Task<CarDTO> CreateAsync(CarDTO entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
        var createdCar = await _repository.CreateAsync(_mapper.Map<Car>(entity));
        return _mapper.Map<CarDTO>(createdCar);
    }

    public async Task<CarDTO> UpdateAsync(CarDTO entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
        var updatedCar = await _repository.UpdateAsync(_mapper.Map<Car>(entity));
        return _mapper.Map<CarDTO>(updatedCar);
    }

    public async Task DeleteAsync(CarDTO entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
        await _repository.DeleteAsync(_mapper.Map<Car>(entity));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing) _repository.Dispose();
        _disposed = true;
    }
}