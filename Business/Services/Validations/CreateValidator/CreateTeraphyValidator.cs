using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreateTeraphyValidator:AbstractValidator<CreateTherapy>
    {
        public CreateTeraphyValidator()
        {
            RuleFor(teraphy => teraphy.TherapyDate).NotNull().NotEmpty().WithMessage("Terapiya tarixi boş ola bilməz");
            RuleFor(teraphy => teraphy.PaymentAmount).NotNull().NotEmpty().WithMessage("Ödəniş məbləği boş ola bilməz");
            RuleFor(teraphy => teraphy.PatientId).NotNull().NotEmpty().WithMessage("Xəstə boş ola bilməz");
            RuleFor(teraphy => teraphy.WorkToBeDone).MaximumLength(2500);
            RuleFor(teraphy => teraphy.WorkDone).MaximumLength(2500);
        }
    }
}
