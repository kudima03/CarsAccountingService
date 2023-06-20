namespace Cars.API.Models.DTOs;

public class CarMainInfoDTO
{
    public long Id { get; set; }

    public string Model { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string RegistrationNumber { get; set; } = null!;

    public string? Colour { get; set; }
}