using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreatePaymentValidator : AbstractValidator<CreatePayment>
    {
        public CreatePaymentValidator()
        {
            RuleFor(payment => payment.PaymentAmount).NotNull().WithMessage("Ödəniş məbləği boş ola bilməz")
                .GreaterThan(0).WithMessage("Ödəniş məbləği 0 dan böyük olmalıdır");
            RuleFor(payment => payment.TherapyId).NotNull().NotEmpty().WithMessage("Müalicə  qeyd olunmalıdır");
            RuleFor(payment => payment.PaymentDate).NotNull().WithMessage("Ödəniş tarixi boş ola bilməz");
        }
    }
}
