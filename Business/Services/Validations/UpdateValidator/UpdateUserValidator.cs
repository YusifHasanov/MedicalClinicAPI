using Entities.Dto.Request.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations.UpdateValidator
{
    public class UpdateUserValidator:AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Id).NotNull().NotEmpty().WithMessage("İstifadəçi Id boş ola bilməz");
            RuleFor(user => user.UserName).NotNull().NotEmpty().WithMessage("Istifadəçi adı boş ola bilməz")
                 .MinimumLength(3).WithMessage("Istifadəçi adı 3 simvoldan az ola bilməz");

            RuleFor(user => user.Password).NotNull().NotEmpty().WithMessage("Istifadəçi adı boş ola bilməz")
                .MinimumLength(5).WithMessage("Istifadəçi adı 5 simvoldan az ola bilməz");

            RuleFor(user => user.Role).IsInEnum().WithMessage("Istifadəçi rolunun dəyəri yanlışdır");
        }
    }
}
