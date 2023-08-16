using Entities.Dto.Request.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.UpdateValidator
{
    public class UpdatePaymentValidator:AbstractValidator<UpdatePayment>
    {
        public UpdatePaymentValidator()
        {
            RuleFor(payment=>payment.Id).NotNull().NotEmpty().WithMessage("Id boş ola bilməz");
            RuleFor(payment => payment.PaymentAmount).NotNull().NotEmpty().WithMessage("Məbləğ boş ola bilməz");
            RuleFor(payment => payment.TherapyId).NotNull().NotEmpty().WithMessage("Terpaiya boş ola bilməz");
            RuleFor(payment => payment.PaymentDate).NotNull().NotEmpty().NotNull().WithMessage("Tarix boş ola bilməz");
        }
    }
}
