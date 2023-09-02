using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreateImageValidator : AbstractValidator<CreateImage>
    {
        private const string PatientShouldBeSelected = "Xəstə seçilməlidir";
        private const string ImageShouldBeSelected = "Şəkil seçilməlidir";
        private const string ImageFormatNotBeValid = "Şəkil formatı düzgün deyil";

        public CreateImageValidator()
        {
            RuleFor(image => image.PatientId)
                .NotNull().WithMessage(PatientShouldBeSelected)
                .NotEmpty().WithMessage(PatientShouldBeSelected)
                .GreaterThan(0).WithMessage(PatientShouldBeSelected);

            RuleFor(image => image.ImageData)
                .NotNull().WithMessage(ImageShouldBeSelected)
                .NotEmpty().WithMessage(ImageShouldBeSelected)
                .Must(BeValidBase64).WithMessage(ImageFormatNotBeValid);
        }

        private bool BeValidBase64(string base64String)
        {
            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
