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
        private const string PaymentAmountShouldNotBeEmpty = "Ödəniş məbləği boş ola bilməz";
        private const string PaymentAmountShouldBeGreaterThanZero = "Ödəniş məbləği 0 dan böyük olmalıdır";
        private const string TherapyShouldNotBeEmpty = "Müalicə  qeyd olunmalıdır";
        private const string PaymentDateShouldNotBeEmpty = "Ödəniş tarixi boş ola bilməz";

        public CreatePaymentValidator()
        {
            RuleFor(payment => payment.PaymentAmount)
                .NotNull().WithMessage(PaymentAmountShouldNotBeEmpty)
                .NotEmpty().WithMessage(PaymentAmountShouldNotBeEmpty)
                .GreaterThan(0).WithMessage(PaymentAmountShouldBeGreaterThanZero);
            RuleFor(payment => payment.TherapyId)
                .NotNull().WithMessage(TherapyShouldNotBeEmpty)
                .NotEmpty().WithMessage(TherapyShouldNotBeEmpty);
            RuleFor(payment => payment.PaymentDate)
                .NotNull().WithMessage(PaymentDateShouldNotBeEmpty);
        }
    }
}
