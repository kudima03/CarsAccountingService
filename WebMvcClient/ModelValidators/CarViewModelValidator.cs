using FluentValidation;
using WebMvcClient.ViewModels;

namespace WebMvcClient.ModelValidators;

public class CarViewModelValidator : AbstractValidator<CarViewModel>
{
    public CarViewModelValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithName("Автомобиль")
            .WithMessage("Car cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithName("Модель")
            .MaximumLength(50);

        RuleFor(x => x.Manufacturer)
            .NotEmpty()
            .WithName("Производитель")
            .MaximumLength(50);

        RuleFor(x => x.YearOfManufacture)
            .GreaterThanOrEqualTo(1900)
            .LessThanOrEqualTo(DateTime.Now.Year)
            .WithName("Год выпуска");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .MaximumLength(15)
            .WithName("Регистрационный номер");

        RuleFor(x => x.VinCode)
            .NotEmpty()
            .Length(17)
            .WithName("Номер VIN");

        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0)
            .WithName("Пробег");

        RuleFor(x => x.Colour)
            .MaximumLength(25)
            .WithName("Цвет");
    }
}