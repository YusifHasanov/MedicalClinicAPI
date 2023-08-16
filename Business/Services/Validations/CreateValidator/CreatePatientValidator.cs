using Entities.Dto.Request.Create;
using Entities.Entities.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreatePatientValidator : AbstractValidator<CreatePatient>
    {
        public CreatePatientValidator()
        {
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
