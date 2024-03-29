﻿using Cars.API.Models.DTOs;
using System.Text;
using System.Text.Json;
using WebMvcClient.Infrastructure;

namespace WebMvcClient.Services;

public class CarsHttpClient : ICarsHttpClient
{
    private readonly string _carsApiUrl;
    private readonly HttpClient _httpClient;

    public CarsHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _carsApiUrl = configuration.GetValue<string>("CarsApiUrl");
    }

    /// <summary>
    ///     Deletes car on appropriate API, specified in config as CarsApiUrl.<br />
    /// </summary>
    /// <param name="carId"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task DeleteCarAsync(int carId)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(URLs.Cars.DeleteCarUrl(_carsApiUrl, carId))
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    ///     Returns all cars from appropriate API, specified in config as CarsApiUrl.<br />
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<List<CarMainInfoDTO>> GetAllCarsAsync()
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(URLs.Cars.GetCarsUrl(_carsApiUrl))
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<CarMainInfoDTO>>(content,
                                                                new JsonSerializerOptions
                                                                {
                                                                    PropertyNameCaseInsensitive = true
                                                                }) ??
               new List<CarMainInfoDTO>();
    }

    public async Task<List<CarMainInfoDTO>> GetCarsRangeAsync(int skip, int take)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(URLs.Cars.GetCarsRangeUrl(_carsApiUrl, skip, take))
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<CarMainInfoDTO>>(content,
                                                                new JsonSerializerOptions
                                                                {
                                                                    PropertyNameCaseInsensitive = true
                                                                }) ??
               new List<CarMainInfoDTO>();
    }

    /// <summary>
    ///     Returns car with <paramref name="carId" /> from appropriate API, specified in config as CarsApiUrl.<br />
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<CarDTO> GetCarAsync(int carId)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(URLs.Cars.GetCarByIdUrl(_carsApiUrl, carId))
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CarDTO>(content,
                                                  new JsonSerializerOptions
                                                  {
                                                      PropertyNameCaseInsensitive = true
                                                  }) ??
               new CarDTO();
    }

    /// <summary>
    ///     Creates car on appropriate API, specified in config as CarsApiUrl.<br />
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public async Task CreateCarAsync(CarDTO car)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(URLs.Cars.PostCarUrl(_carsApiUrl)),
            Content = new StringContent(JsonSerializer.Serialize(car), Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    ///     Updates car on appropriate API, specified in config as CarsApiUrl.<br />
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public async Task UpdateCarAsync(CarDTO car)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(URLs.Cars.UpdateCarUrl(_carsApiUrl)),
            Content = new StringContent(JsonSerializer.Serialize(car), Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}