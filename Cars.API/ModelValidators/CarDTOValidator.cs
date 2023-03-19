using Cars.API.Models.DTOs;
using FluentValidation;

namespace Cars.API.ModelValidators;

public class CarDTOValidator : AbstractValidator<CarDTO>
{
    public CarDTOValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Car cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Manufacturer)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.YearOfManufacture)
            .GreaterThanOrEqualTo(1900)
            .LessThanOrEqualTo(DateTime.Now.Year);

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.VinCode)
            .NotEmpty()
            .Length(17);

        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Colour)
            .MaximumLength(25);
    }
}