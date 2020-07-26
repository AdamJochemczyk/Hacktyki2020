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
        RuleFor(p=>p).Must(p => p.Email != null).WithMessage("Not match email");
        RuleFor(p => p).Must(p => p.EncodePassword != null).WithMessage("Not match password");
        }
    }
}
