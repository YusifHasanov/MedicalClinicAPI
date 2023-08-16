using Entities.Dto.Request.Update;
using Entities.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.UpdateValidator
{
    public class UpdateDoctorValidator:AbstractValidator<UpdateDoctor>
    {
        public UpdateDoctorValidator()
        {
            RuleFor(doctor => doctor.Id).NotNull().NotEmpty().WithMessage("Həkim Id boş ola bilməz");
            RuleFor(doctor => doctor.Name).NotNull().NotEmpty().WithMessage("Həkim Adı boş ola bilməz");
            RuleFor(doctor => doctor.Surname).NotNull().NotEmpty().WithMessage("Həkim Soyadı boş ola bilməz");
        }

    }
}
