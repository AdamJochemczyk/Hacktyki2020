using CarRental.Services.Models.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.MobileNumber).Length(9);
            RuleFor(u => u.NumberIdentificate).Length(6);
           
        }
    }
}
