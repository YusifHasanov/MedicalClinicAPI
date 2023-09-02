using Entities.Dto;
using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Validators;

namespace Business.Services.Validations.CreateValidator
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        private const string UserNameCannotBeEmpty = "Istifadəçi adı boş ola bilməz";
        private const string UserNameCannotBeLessThan3 = "Istifadəçi adı 3 simvoldan az ola bilməz";
        private const string PasswordCannotBeEmpty = "Istifadəçi parolu boş ola bilməz";
        private const string PasswordCannotBeLessThan5 = "Istifadəçi parolu 5 simvoldan az ola bilməz";
        private const string RoleValueIsNotValid = "Istifadəçi rolunun dəyəri yanlışdır";
        public CreateUserValidator()
        {
            RuleFor(user => user.UserName)
                .NotNull().WithMessage(UserNameCannotBeEmpty)
                .NotEmpty().WithMessage(UserNameCannotBeEmpty)
                .MinimumLength(3).WithMessage(UserNameCannotBeLessThan3);

            RuleFor(user => user.Password)
                .NotNull().WithMessage(PasswordCannotBeEmpty)
                .NotEmpty().WithMessage(PasswordCannotBeEmpty)
                .MinimumLength(5).WithMessage(PasswordCannotBeLessThan5);

            RuleFor(user => user.Role).IsInEnum().WithMessage(RoleValueIsNotValid);
        }
    }
}
