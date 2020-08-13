using CarRental.Services.Models.Defect;
using FluentValidation;

namespace CarRental.Services.Validators
{
    public class UpdateDefectDtoValidator:AbstractValidator<UpdateDefectDto>
    {
        public UpdateDefectDtoValidator()
        {
            RuleFor(p => p.Description).NotEmpty().MaximumLength(250);
        }
    }
}
