namespace Cars.API.Models.DTOs;

public class CarDTO
{
    public long Id { get; set; }

    public string Model { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public int YearOfManufacture { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public string VinCode { get; set; } = null!;

    //Mileage in kilometers
    public int Mileage { get; set; }

    public string? Colour { get; set; }
}