using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Validations
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState
                      .Where(x => x.Value.Errors.Any())
                      .Select(e => new
                      {
                          e.Key,
                          Message = e.Value.Errors.Select(x => x.ErrorMessage)
                      }).ToList();

                context.Result = new BadRequestObjectResult(error);
                return;
            }
            await next();
        }
    }
}
