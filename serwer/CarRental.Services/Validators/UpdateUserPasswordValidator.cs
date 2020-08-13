using CarRental.Services.Models.User;
using FluentValidation;

namespace CarRental.Services.Validators
{
    public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordDto>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(p => p).Must(p => p.EncodePassword.Length <= 8).WithMessage("Password so weakly");
            RuleFor(p => p).Must(p => p.ConfirmEncodePassword == p.EncodePassword).WithMessage("Paaword is not the same");
        }
    }
}
