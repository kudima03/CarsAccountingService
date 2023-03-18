using System.Net.Mime;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cars.API.Data.Interfaces;
using Cars.API.Models;
using Cars.API.Models.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly IAsyncRepository<Car> _carsRepository;

    private readonly IValidator<CarDTO> _carsValidator;

    private readonly IMapper _mapper;

    public CarsController(IAsyncRepository<Car> eventContext, IValidator<CarDTO> eventValidator, IMapper mapper)
    {
        _carsRepository = eventContext;
        _carsValidator = eventValidator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarMainInfoDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarMainInfoDTO>>> CarsAsync()
    {
        var events = await _carsRepository.GetAllAsync();
        return Ok(events.AsQueryable().ProjectTo<CarMainInfoDTO>(_mapper.ConfigurationProvider).AsEnumerable());
    }

    [HttpGet("{skip:int}:{take:int}")]
    [ProducesResponseType(typeof(IEnumerable<CarMainInfoDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarMainInfoDTO>>> CarsRangeAsync([FromQuery] int skip,
        [FromQuery] int take)
    {
        var events = await _carsRepository.GetRangeAsync(skip, take);
        return Ok(events.AsQueryable().ProjectTo<CarMainInfoDTO>(_mapper.ConfigurationProvider).AsEnumerable());
    }

    [HttpGet("{carId:int}")]
    [ProducesResponseType(typeof(CarDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarDTO>> CarByIdAsync(int carId)
    {
        if (carId <= 0) return BadRequest("Id cannot be less than zero.");

        var car = await _carsRepository.GetByIdAsync(carId);

        if (car == null) return NotFound($"Car with id:{carId} not found.");

        return Ok(_mapper.Map<CarDTO>(car));
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarAsync([FromBody] CarDTO car)
    {
        var validationResult = await _carsValidator.ValidateAsync(car);
        if (!validationResult.IsValid) return BadRequest(validationResult.ToString());

        car.Id = 0;
        await _carsRepository.CreateAsync(_mapper.Map<Car>(car));
        return Ok();
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCarAsync([FromBody] CarDTO car)
    {
        var validationResult = await _carsValidator.ValidateAsync(car);
        if (!validationResult.IsValid) return BadRequest(validationResult.ToString());

        var entity = _carsRepository.GetById(car.Id);

        if (entity == null)
        {
            car.Id = 0;
            await _carsRepository.CreateAsync(_mapper.Map<Car>(car));
            return Ok();
        }

        await _carsRepository.UpdateAsync(_mapper.Map<Car>(car));
        return Ok();
    }

    [HttpDelete("{carId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCarAsync(int carId)
    {
        if (carId <= 0) return BadRequest("Id cannot be less than zero.");

        var entityToDelete = await _carsRepository.GetByIdAsync(carId);
        if (entityToDelete == null) return NotFound("Car not found.");

        await _carsRepository.DeleteAsync(entityToDelete);
        return Ok();
    }
}