using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.CreateValidator
{
    public class CreateİmageValidator:AbstractValidator<CreateImage>
    {
        public CreateİmageValidator()
        {
            RuleFor(image => image.ImageData).NotNull().NotEmpty().WithMessage("Şəkil boş ola bilməz");
            RuleFor(image => image.PatientId).NotNull().NotEmpty().WithMessage("Xəstə boş ola bilməz");
        }
    }
}
