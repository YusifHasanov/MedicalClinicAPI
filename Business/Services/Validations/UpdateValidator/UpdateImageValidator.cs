using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.UpdateValidator
{
    public class UpdateImageValidator:AbstractValidator<UpdateImage>
    {
        public UpdateImageValidator()
        {
            RuleFor(image => image.Id).NotNull().NotEmpty().WithMessage("Şəkil Id boş ola bilməz");
            RuleFor(image => image.ImageData).NotNull().NotEmpty().WithMessage("Şəkil boş ola bilməz");
            RuleFor(image => image.PatientId).NotNull().NotEmpty().WithMessage("Xəstə boş ola bilməz");
        }
    }
}
