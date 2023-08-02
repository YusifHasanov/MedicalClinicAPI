using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations
{
    public class CreatePaymentValidator : AbstractValidator<CreatePayment>
    {
        public CreatePaymentValidator()
        {
            RuleFor(payment => payment.Amount).NotNull().GreaterThan(0);
            RuleFor(payment => payment.PatientId).NotNull().NotEmpty();
        }
    }
}
