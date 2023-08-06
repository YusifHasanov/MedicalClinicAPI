using AutoMapper;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Services
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<CreateImage, Image>();
            CreateMap<UpdateImage, Image>();
            CreateMap<Image, ImageResponse>();


            CreateMap<CreatePatient, Patient>();
            CreateMap<UpdatePatient, Patient>();
            CreateMap<Patient, PatientResponse>()
          .ForMember(dest => dest.PayedAmount, opt => opt.MapFrom(src => src.Payments.Sum(payment => payment.Amount)));

            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();
            CreateMap<User, UserResponse>();

            CreateMap<CreatePayment, Payment>();
            CreateMap<UpdatePayment, Payment>();
            CreateMap<Payment, PaymentResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.Name))
                .ForMember(dest => dest.PatientSurname, opt => opt.MapFrom(src => src.Patient.Surname))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Patient.Doctor.Name))
                .ForMember(dest => dest.DoctorSurname, opt => opt.MapFrom(src => src.Patient.Doctor.Surname));


            CreateMap<CreateDoctor, Doctor>();
            CreateMap<UpdateDoctor, Doctor>();
            CreateMap<Doctor, DoctorResponse>()
                .ForMember(dest => dest.TotalPayment, opt => opt
                .MapFrom( src => src.Patients.SelectMany(patient => patient.Payments)
                    .Sum(payment => payment.Amount)));

        }
    }
}
