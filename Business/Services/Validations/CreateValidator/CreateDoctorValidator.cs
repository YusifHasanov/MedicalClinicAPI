using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreateDoctorValidator : AbstractValidator<CreateDoctor>
    {
        private const string DoctorNameShouldNotBeEmpty = "Həkim Adı boş ola bilməz";
        private const string DoctorSurnameShouldNotBeEmpty = "Həkim Soyadı boş ola bilməz";
        public CreateDoctorValidator()
        {
            RuleFor(doctor => doctor.Name)
                .NotNull().WithMessage(DoctorNameShouldNotBeEmpty)
                .NotEmpty().WithMessage(DoctorNameShouldNotBeEmpty);

            RuleFor(doctor => doctor.Surname)
                .NotNull().WithMessage(DoctorSurnameShouldNotBeEmpty)
                .NotEmpty().WithMessage(DoctorSurnameShouldNotBeEmpty);
        }
    }
}
