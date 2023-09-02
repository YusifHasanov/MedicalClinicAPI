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
        private const string PatientShouldNotBeEmpty = "Xəstə seçilməlidir"; 
        private const string TeraphyDateShouldNotBeEmpty = "Terapiya tarixi boş ola bilməz"; 
        private const string PaymentAmountShouldNotBeEmpty = "Ödəniş məbləği boş ola bilməz";  
        private const string WorkDoneMaxLength = "Edilən işlər 2500 simvoldan çox ola bilməz";  
        private const string WorkToBeDoneMaxLength = "Ediləcək işlər 2500 simvoldan çox ola bilməz";
         
        public CreateTeraphyValidator()
        {
            RuleFor(teraphy => teraphy.TherapyDate)
                .NotNull().WithMessage(TeraphyDateShouldNotBeEmpty)
                .NotEmpty().WithMessage(TeraphyDateShouldNotBeEmpty);

            RuleFor(teraphy => teraphy.PaymentAmount)
                .NotNull().WithMessage(PaymentAmountShouldNotBeEmpty)
                .NotEmpty().WithMessage(PaymentAmountShouldNotBeEmpty);

            RuleFor(teraphy => teraphy.PatientId)
                .NotNull().WithMessage(PatientShouldNotBeEmpty)
                .NotEmpty().WithMessage(PatientShouldNotBeEmpty);

            RuleFor(teraphy => teraphy.WorkToBeDone).MaximumLength(2500).WithMessage(WorkToBeDoneMaxLength);
            RuleFor(teraphy => teraphy.WorkDone).MaximumLength(2500).WithMessage(WorkDoneMaxLength);
        }
    }
}
