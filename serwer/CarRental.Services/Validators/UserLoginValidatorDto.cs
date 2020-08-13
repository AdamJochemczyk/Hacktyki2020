using CarRental.Services.Models.User;
using FluentValidation;

namespace CarRental.Services.Validators
{
    public class UserLoginValidatorDto : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidatorDto()
        {
            RuleFor(p => p).Must(p => p.Email != null).WithMessage("Email not correct");
            RuleFor(p => p).Must(p => p.Password != null).WithMessage("Email not correct");
        }
    }
}
