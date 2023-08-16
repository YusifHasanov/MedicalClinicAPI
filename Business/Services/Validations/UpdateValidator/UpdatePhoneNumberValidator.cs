using Entities.Dto.Request.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.UpdateValidator
{
    public class UpdatePhoneNumberValidator :AbstractValidator<UpdatePhoneNumber>
    {
        public UpdatePhoneNumberValidator()
        {
            RuleFor(phoneNumber => phoneNumber.Id).NotNull().NotEmpty().WithMessage("Nömrə Id boş ola bilməz");
            RuleFor(phoneNumber => phoneNumber.PatientId).NotNull().NotEmpty().WithMessage("Xəstə boş ola bilməz");
        }
  
    }
}
