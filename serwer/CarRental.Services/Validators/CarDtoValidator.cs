using CarRental.DAL.Entities;
using CarRental.Services.Models.Car;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Validators
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(p => p.Brand).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(p => p.Model).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(p => p.RegistrationNumber).NotNull().NotEmpty().MaximumLength(7);
            RuleFor(p => p.NumberOfDoor).NotNull().NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
            RuleFor(p => p.NumberOfSits).NotNull().NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(9);
            RuleFor(p => p.YearOfProduction).NotNull().NotEmpty().GreaterThanOrEqualTo(1950).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(p => p.TypeOfCar).IsInEnum();
        }
    }
}
