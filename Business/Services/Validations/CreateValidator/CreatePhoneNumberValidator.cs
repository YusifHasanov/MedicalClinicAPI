using Entities.Dto.Request.Create;
using Entities.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreatePhoneNumberValidator:AbstractValidator<CreatePhoneNumber>
    {
        public CreatePhoneNumberValidator()
        {
            RuleFor(phoneNumber => phoneNumber.PatientId).NotNull().NotEmpty().WithMessage("Xəstə boş ola bilməz");
        }
    }
}
