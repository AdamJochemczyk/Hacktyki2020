using CarRental.Services.Models.User;
using FluentValidation;
namespace CarRental.Services.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(p => p).Must(p => p.Email != null).WithMessage("Email not correct");
            RuleFor(p => p).Must(p => p.MobileNumber.Length<9).WithMessage("Mobile not correct");
            RuleFor(p => p).Must(p => p.MobileNumber.Length > 9).WithMessage("Mobile not correct");
            RuleFor(p => p).Must(p => p.IdentificationNumber.Length > 6).WithMessage("Indentification number is not correct");
            RuleFor(p => p).Must(p => p.IdentificationNumber.Length < 6).WithMessage("Indentification number is not correct");
        }
    }
}
