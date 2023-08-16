using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreateDoctorValidator:AbstractValidator<CreateDoctor>
    {
        public CreateDoctorValidator()
        {
            RuleFor(doctor => doctor.Name).NotNull().NotEmpty().WithMessage("Həkim Adı boş ola bilməz");
            RuleFor(doctor => doctor.Surname).NotNull().NotEmpty().WithMessage("Həkim Soyadı boş ola bilməz");
        }
    }
}
