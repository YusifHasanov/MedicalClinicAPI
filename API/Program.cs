using Core.Utils;
using Core.Utils.Middleware;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
 
var builder = WebApplication.CreateBuilder(args);

builder.AddServices(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 
var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<DbEntity>();
//    if (context.Database.GetPendingMigrations().Any())
//    {
//        context.Database.Migrate();
//    }
//}


app.UseSwagger();
app.UseSwaggerUI();  
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("MedicalCors");
//app.UseMiddleware<BasicAuthenticationMiddleware>();
app.Run();
