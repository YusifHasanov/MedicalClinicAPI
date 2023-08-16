using Entities.Dto.Request.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.UpdateValidator
{
    public class UpdatePatientValidator:AbstractValidator<UpdatePatient>
    {
        public UpdatePatientValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("Xəstə Id boş ola bilməz");
            RuleFor(p => p.Name).MaximumLength(55).NotNull().NotEmpty().WithMessage("Ad bos ola bilməz");
            RuleFor(p => p.Surname).MaximumLength(55).NotNull().NotEmpty().WithMessage("Soyad bos ola bilməz");
            RuleFor(p => p.Adress).MaximumLength(1000);
            RuleFor(p => p.Diagnosis).MaximumLength(1000);

            RuleFor(p => p.GeneralStateOfHealth).MaximumLength(500);
            RuleFor(p => p.DrugAllergy).MaximumLength(500);
            RuleFor(p => p.ReactionToAnesthesia).MaximumLength(500);
            RuleFor(p => p.InjuryProblem).MaximumLength(500);
            RuleFor(p => p.DelayedSurgeries).MaximumLength(500);
            RuleFor(p => p.Bleeding).MaximumLength(500);
            RuleFor(p => p.PregnancyStatus);
        }   
    }
}
