using Entities.Dto.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Validations
{
    public class DateIntervalRequestValidator:AbstractValidator<DateIntervalRequest>
    {
        public DateIntervalRequestValidator()
        {
 
        }
    }
}
