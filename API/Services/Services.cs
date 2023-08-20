using Business.Abstract;
using Business.Concrete;
using Business.Services;
using Business.Services.BackgroundServices;
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
            builder.Services.AddControllers(options => options.Filters.Add<FluentValidationFilter>())
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CreatePatientValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            //builder.Services.AddDataProtection()
            //        .PersistKeysToFileSystem(new DirectoryInfo(@".\AppData"))
            //        //.ProtectKeysWithCertificate("thumbprint")
            //        .SetDefaultKeyLifetime(TimeSpan.FromDays(7))
            //        .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
            //        {
            //            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            //            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            //        });
            //builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            //               .ReadFrom.Configuration(hostingContext.Configuration)
            //                              .Enrich.FromLogContext().CreateBootstrapLogger());

            //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(_configuration).CreateLogger();
            //builder.Host.UseSerilog();

            builder.Services.AddSingleton<Globals, Globals>();
            builder.Services.AddSingleton<IValidator<CreatePatient>, CreatePatientValidator>();
            builder.Services.AddAutoMapper(typeof(ModelMapper));
            builder.Services.AddScoped<ILogService, LogService>();

 

            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();

            builder.Services.AddScoped<ITherapyRepository, TherapyRepository>();
            builder.Services.AddScoped<ITherapyService, TherapyService>();

            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            builder.Services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();

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
                    policy.WithOrigins("http://localhost:5000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            builder.Services.AddHostedService<MonthlyTruncateLogTable>();
        }

    }
}
