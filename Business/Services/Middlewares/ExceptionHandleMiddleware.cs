using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Core.Utils.Exceptions;

namespace Business.Services.Middlewares
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExceptionHandleMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedException ex)
            {
                await GenerateErrorResponse(context, ex, HttpStatusCode.Unauthorized);
            }
            catch (NotFoundException ex)
            {
                await GenerateErrorResponse(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await GenerateErrorResponse(context, ex, HttpStatusCode.BadRequest);
               
            }
        }

        private async Task GenerateErrorResponse(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int) statusCode;

            var respnonse = new ErrorResponse()
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode,
            };

            var json = JsonSerializer.Serialize(respnonse);

            await context.Response.WriteAsync(json);
        }
    }
}
