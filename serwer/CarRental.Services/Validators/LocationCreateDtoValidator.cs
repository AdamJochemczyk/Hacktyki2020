using CarRental.Services.Models.Location;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Validators
{
    public class LocationCreateDtoValidator : AbstractValidator<LocationCreateDto>
    {
        public LocationCreateDtoValidator()
        {
            RuleFor(p => p.ReservationId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(p => p.Longitude)
                .GreaterThanOrEqualTo(-180)
                .LessThanOrEqualTo(180)
                .NotEmpty();
            RuleFor(p => p.Latitude)
                .GreaterThanOrEqualTo(-90)
                .LessThanOrEqualTo(90)
                .NotEmpty();
        }
    }
}
