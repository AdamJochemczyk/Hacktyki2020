using CarRental.Services.Models.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Validators
{
    public class UserLoginValidatorDto : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidatorDto() { 
        RuleFor(p=>p).Must(p => p.Email == null).WithMessage("Email not correct");
        RuleFor(p => p).Must(p => p.EncodePassword == null).WithMessage("Email not correct");
        }
    }
}
