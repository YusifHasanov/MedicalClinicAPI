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
        private const string PatientShouldNotBeEmpty = "Xəstə seçilməlidir";
        public CreatePhoneNumberValidator()
        {
            RuleFor(phoneNumber => phoneNumber.PatientId)
                .NotNull().WithMessage(PatientShouldNotBeEmpty)
                .NotEmpty().WithMessage(PatientShouldNotBeEmpty);
        }
    }
}
