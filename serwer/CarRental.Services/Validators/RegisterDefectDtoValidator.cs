using CarRental.Services.Models.Defect;
using FluentValidation;

namespace CarRental.Services.Validators
{
    public class RegisterDefectDtoValidator: AbstractValidator<RegisterDefectDto>
    {
      public RegisterDefectDtoValidator()
        {
            RuleFor(p => p).Must(p => p.Description.Length > 250).WithMessage("So long message");
            RuleFor(p => p).Must(p => p.Description.Length < 5).WithMessage("So short message");
        }
    }
}
