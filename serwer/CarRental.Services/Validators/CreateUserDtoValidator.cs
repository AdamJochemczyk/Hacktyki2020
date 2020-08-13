using CarRental.Services.Models.User;
using FluentValidation;


namespace CarRental.Services.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress().MinimumLength(5);
            RuleFor(p => p.MobileNumber).NotEmpty().Length(9);
            RuleFor(p => p.IdentificationNumber).Length(6);
        }
    }
}
