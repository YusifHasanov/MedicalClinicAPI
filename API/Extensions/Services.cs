using Business.Abstract;
using Business.Concrete;
using Business.Services;
using Business.Services.BackgroundServices;
using Business.Services.Middlewares;
using Business.Services.Validations.CreateValidator;
using Core.Utils.Constants;
using Core.Validations;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dto.Request.Create;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Core.Utils
{
    public static class Services
    {

        static IConfiguration _configuration = new ConfigurationBuilder()
                 //.SetBasePath("")  
                 .AddJsonFile("appsettings.json").Build();

        public static void AddServices(this WebApplicationBuilder builder)
        {
            
             
            builder.Services.AddDbContext<DbEntity>();
            builder.Services.AddControllers(options => { 
                options.Filters.Add<FluentValidationFilter>(); 
            })
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CreatePatientValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddSingleton<Globals, Globals>();
            builder.Services.AddSingleton<IValidator<CreatePatient>, CreatePatientValidator>();
            builder.Services.AddAutoMapper(typeof(ModelMapper));

            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddHealthChecks();
            builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
      {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = _configuration["JWT:Issuer"] ?? "",
              ValidAudience = _configuration["JWT:Audience"] ?? "",
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"] ?? ""))
          };
      });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MedicalCors", policy =>
                {
                    policy.WithOrigins("http://localhost:5556", "https://localhost:5556", "https://medical-clinic-ui-yusifhasanov.vercel.app", "https://medical-clinic-ui-git-main-yusifhasanov.vercel.app", "https://medical-clinic-ui.vercel.app")
                          .AllowAnyHeader()
                          .AllowAnyMethod() 
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();

                    //policy.WithOrigins("https://medical-clinic-ui.vercel.app")
                    //       .AllowAnyMethod()
                    //       .WithExposedHeaders("content-disposition")
                    //       .SetIsOriginAllowed((host) => true)
                    //       .AllowAnyHeader()
                    //       .AllowCredentials()
                    //       .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
                    //policy.AllowAnyOrigin()
                    //       .AllowAnyMethod()
                    //       .AllowAnyHeader()
                    //       .AllowCredentials();



                });
            });
            builder.Services.AddHostedService<WeeklyTruncateLogsTable>();
        }

        public static void MigrateIfPendingMigrations(this WebApplication app)
        {
            try
            {
                using var migrationScope = app.Services.CreateScope();
                var serviceProvider = migrationScope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<DbEntity>();
                var pendingMigrations = dbContext.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {
                    dbContext.Database.Migrate();
                }
            }
            catch
            {
            }
        }
    }
}
