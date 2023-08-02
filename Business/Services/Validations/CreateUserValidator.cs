using Entities.Dto;
using Entities.Dto.Request.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Validators;
namespace Business.Services.Validations
{
    public class CreateUserValidator:AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.UserName).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(user=>user.Password).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(user => user.Role).IsInEnum();
        }
    }
}
