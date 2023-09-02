using Business.Services.Middlewares;
using Core.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MigrateIfPendingMigrations();
app.UseHttpsRedirection();
app.UseCors("MedicalCors");
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHealthChecks("/check");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<BasicAuthenticationMiddleware>();
app.UseMiddleware<ExceptionHandleMiddleware>();
app.MapControllers();

app.Run();
