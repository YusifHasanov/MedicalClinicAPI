using Business.Abstract;
using Core.Entities;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Services.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RequestLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Intercept the response stream
            var originalResponseBody = context.Response.Body;
            using var responseBodyCaptureStream = new MemoryStream();
            context.Response.Body = responseBodyCaptureStream;

            try
            {
                await _next(context);

                responseBodyCaptureStream.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(responseBodyCaptureStream, Encoding.UTF8);
                var responseBodyContent = await reader.ReadToEndAsync();

                context.Request.EnableBuffering();
                using var scope = _serviceScopeFactory.CreateScope();
                var logService = scope.ServiceProvider.GetRequiredService<ILogService>();

                var responseCode = context.Response.StatusCode;

                bool hasError = responseCode >= 400;

                string? message = hasError ? null : responseBodyContent;

                await logService.LogAsync(new Log()
                {
                    LogDate = DateTime.Now,
                    LogLevel = hasError ? LogLevel.Error.ToString() : LogLevel.Info.ToString(),
                    ResponseCode = responseCode,
                    Path = $"Request: {context.Request.Method} {context.Request.Path}",
                    Message = hasError ? responseBodyContent : message,
                });

                responseBodyCaptureStream.Seek(0, SeekOrigin.Begin);
                await responseBodyCaptureStream.CopyToAsync(originalResponseBody);
            }
            catch
            {
            }
        }
    }
}
