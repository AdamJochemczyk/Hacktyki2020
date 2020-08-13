using CarRental.Services.Models.Defect;
using FluentValidation;

namespace CarRental.Services.Validators
{
    public class RegisterDefectDtoValidator: AbstractValidator<RegisterDefectDto>
    {
      public RegisterDefectDtoValidator()
        {
            RuleFor(p => p.Description).NotEmpty().MaximumLength(250);
        }
    }
}
