using FluentValidation;
using WebMvcClient.ViewModels;

namespace WebMvcClient.ModelValidators;

public class CarViewModelValidator : AbstractValidator<CarViewModel>
{
    public CarViewModelValidator()
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
            .MaximumLength(17);

        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Colour)
            .MaximumLength(25);
    }
}