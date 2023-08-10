
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Services;
using Business.Services.Validations;
using Core.Utils.Constants;
using Core.Validations;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dto.Request.Create;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.IdentityModel.Tokens;

using System.Text;


namespace Core.Utils
{
    public static class Services
    {

        static IConfiguration _configuration = new ConfigurationBuilder()
                 //.SetBasePath("")  
                 .AddJsonFile("appsettings.json").Build();

        public static void AddServices(this IServiceCollection services)
        {

            services.AddDbContext<DbEntity>();
            services.AddControllers(options => options.Filters.Add<FluentValidationFilter>())
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CreatePatientValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            //services.AddDataProtection()
            //        .PersistKeysToFileSystem(new DirectoryInfo(@".\AppData"))
            //        //.ProtectKeysWithCertificate("thumbprint")
            //        .SetDefaultKeyLifetime(TimeSpan.FromDays(7))
            //        .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
            //        {
            //            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            //            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            //        });

            services.AddSingleton<IValidator<CreatePatient>, CreatePatientValidator>();
            services.AddAutoMapper(typeof(ModelMapper));
            services.AddSingleton<ILogService, LogService>();

            services.AddSingleton<Globals, Globals>();

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();

            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();

            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
             

            services.AddAuthentication(options =>
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MedicalCors", policy =>
                {
                    policy.WithOrigins("http://localhost:5000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}
