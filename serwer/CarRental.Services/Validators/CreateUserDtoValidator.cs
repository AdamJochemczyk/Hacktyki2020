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
            RuleFor(p => p).Must(p => p.NumberIdentificate.Length > 6).WithMessage("Number of indentification not correct");
            RuleFor(p => p).Must(p => p.NumberIdentificate.Length < 6).WithMessage("Number of indentification not correct");
        }
    }
}
