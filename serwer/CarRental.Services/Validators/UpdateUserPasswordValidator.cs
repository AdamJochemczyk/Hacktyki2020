using CarRental.Services.Models.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Validators
{
    public class UpdateUserPasswordValidator: AbstractValidator<UpdateUserPasswordDto>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(p => p.EncodePassword).NotNull().MinimumLength(8);
        }
    }
}
