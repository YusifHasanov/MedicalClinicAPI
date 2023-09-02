using Entities.Dto.Request.Create;
using Entities.Entities.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreatePatientValidator : AbstractValidator<CreatePatient>
    {

        private const string PatientNameShouldNotBeEmpty = "Ad boş ola bilməz";
        private const string PatientSurnameShouldNotBeEmpty = "Soyad boş ola bilməz";
        private const string PatientNameMaxLength = "Ad 55 simvoldan çox ola bilməz";
        private const string PatientSurnameMaxLength = "Soyad 55 simvoldan çox ola bilməz";
        private const string PatientAddressMaxLength = "Ünvan 1000 simvoldan çox ola bilməz";
        private const string PatientDiagnosisMaxLength = "Diaqnoz 1000 simvoldan çox ola bilməz";
        private const string PatientGeneralStateOfHealthMaxLength = "Ümumi sağlamlıq vəziyyəti 500 simvoldan çox ola bilməz";
        private const string PatientDrugAllergyMaxLength = "Dərman alerjisi 500 simvoldan çox ola bilməz";
        private const string PatientReactionToAnesthesiaMaxLength = "Anesteziyaya reaksiya 500 simvoldan çox ola bilməz";
        private const string PatientInjuryProblemMaxLength = "Travma problemləri 500 simvoldan çox ola bilməz";
        private const string PatientDelayedSurgeriesMaxLength = "Gecikmiş əməliyyatlar 500 simvoldan çox ola bilməz";
        private const string PatientBleedingMaxLength = "Xəstənin qanamağı 500 simvoldan çox ola bilməz";


        public CreatePatientValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(55).WithMessage(PatientNameMaxLength)
                .NotNull().WithMessage(PatientNameShouldNotBeEmpty)
                .NotEmpty().WithMessage(PatientNameShouldNotBeEmpty);

            RuleFor(p => p.Surname)
                .MaximumLength(55).WithMessage(PatientSurnameMaxLength)
                .NotNull().NotEmpty().WithMessage(PatientSurnameShouldNotBeEmpty);
            RuleFor(p => p.Address).MaximumLength(1000).WithMessage(PatientAddressMaxLength);
            RuleFor(p => p.Diagnosis).MaximumLength(1000).WithMessage(PatientDiagnosisMaxLength);

            RuleFor(p => p.GeneralStateOfHealth).MaximumLength(500).WithMessage(PatientGeneralStateOfHealthMaxLength);
            RuleFor(p => p.DrugAllergy).MaximumLength(500).WithMessage(PatientDrugAllergyMaxLength);
            RuleFor(p => p.ReactionToAnesthesia).MaximumLength(500).WithMessage(PatientReactionToAnesthesiaMaxLength);
            RuleFor(p => p.InjuryProblem).MaximumLength(500).WithMessage(PatientInjuryProblemMaxLength);
            RuleFor(p => p.DelayedSurgeries).MaximumLength(500).WithMessage(PatientDelayedSurgeriesMaxLength);
            RuleFor(p => p.Bleeding).MaximumLength(500).WithMessage(PatientBleedingMaxLength);
            RuleFor(p => p.PregnancyStatus);
        }

    }
}
