using System.Net.Mime;
using Cars.API.Models.DTOs;
using Cars.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarsService _carsService;

    private readonly ILogger<CarsController> _logger;

    public CarsController(ICarsService carsService, ILogger<CarsController> logger)
    {
        _carsService = carsService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarMainInfoDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<CarMainInfoDTO>>> CarsAsync()
    {
        try
        {
            return Ok(await _carsService.GetAllAsync());
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpGet("range")]
    [ProducesResponseType(typeof(IEnumerable<CarMainInfoDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<CarMainInfoDTO>>> CarsRangeAsync([FromQuery] int fromInclusive,
        [FromQuery] int toExclusive)
    {
        if (fromInclusive < 0 || toExclusive < 0) return BadRequest("Index cannot be less than zero.");
        try
        {
            return Ok(await _carsService.GetRangeAsync(fromInclusive, toExclusive));
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpGet("{carId:int}")]
    [ProducesResponseType(typeof(CarDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<CarDTO>> CarByIdAsync(int carId)
    {
        try
        {
            if (carId <= 0) return BadRequest("Id cannot be less than zero.");

            var car = await _carsService.GetByIdAsync(carId);

            if (car == null) return NotFound($"Car with id:{carId} not found.");

            return Ok(car);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CreateCarAsync([FromBody] CarDTO car)
    {
        try
        {
            await _carsService.CreateAsync(car);
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> UpdateCarAsync([FromBody] CarDTO car)
    {
        try
        {
            var entity = await _carsService.GetByIdAsync(car.Id);

            if (entity == null)
            {
                await _carsService.CreateAsync(car);
                return Ok();
            }

            await _carsService.UpdateAsync(car);
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpDelete("{carId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeleteCarAsync(int carId)
    {
        try
        {
            if (carId <= 0) return BadRequest("Id cannot be less than zero.");

            var entityToDelete = await _carsService.GetByIdAsync(carId);
            if (entityToDelete == null) return NotFound("Car not found.");

            await _carsService.DeleteAsync(entityToDelete);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}