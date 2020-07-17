using CarRental.DAL.Entities;
using CarRental.Services.Models.Reservation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Validators
{
    public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateDto>
    {
        public ReservationCreateDtoValidator()
        {
            RuleFor(p => p.CarId).NotNull().GreaterThan(0);
            RuleFor(p => p.UserId).NotNull().GreaterThan(0);
            RuleFor(p => p.RentalDate).GreaterThanOrEqualTo(DateTime.Now)
                .NotEmpty().NotNull();
            RuleFor(p => p.ReturnDate).GreaterThan(p => p.RentalDate)
                .NotEmpty().NotNull();
        }
    }
}
