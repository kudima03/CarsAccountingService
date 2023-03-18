using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cars.API.Models.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvcClient.Services;
using WebMvcClient.ViewModels;

namespace WebMvcClient.Controllers;

[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]/[action]")]
public class CarsController : Controller
{
    private readonly ICarsHttpClient _eventsHttpClient;

    private readonly IMapper _mapper;

    private readonly IValidator<CarViewModel> _validator;

    public CarsController(ICarsHttpClient eventsHttpClient, IMapper mapper, IValidator<CarViewModel> validator)
    {
        _eventsHttpClient = eventsHttpClient;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cars = await _eventsHttpClient.GetAllCarsAsync();
        return View(cars.AsQueryable().ProjectTo<CarMainInfoViewModel>(_mapper.ConfigurationProvider).AsEnumerable());
    }

    [HttpGet]
    public async Task<IActionResult> CarDetails(int carId)
    {
        return View(_mapper.Map<CarViewModel>(await _eventsHttpClient.GetCarAsync(carId)));
    }

    [HttpGet]
    public IActionResult CreateCar()
    {
        return View("CreateOrUpdateCar", new CarViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCar([FromForm] CarViewModel car)
    {
        var validationResult = await _validator.ValidateAsync(car);
        if (!validationResult.IsValid) return View("ValidationErrors", validationResult.Errors);
        await _eventsHttpClient.CreateCarAsync(_mapper.Map<CarDTO>(car));
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCar(int carId)
    {
        return View("CreateOrUpdateCar", _mapper.Map<CarViewModel>(await _eventsHttpClient.GetCarAsync(carId)));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCar([FromForm] CarViewModel car)
    {
        var validationResult = await _validator.ValidateAsync(car);
        if (!validationResult.IsValid) return View("ValidationErrors", validationResult.Errors);
        await _eventsHttpClient.UpdateCarAsync(_mapper.Map<CarDTO>(car));
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCar(int carId)
    {
        await _eventsHttpClient.DeleteCarAsync(carId);
        return RedirectToAction(nameof(Index));
    }
}