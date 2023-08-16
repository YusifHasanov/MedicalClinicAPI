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


            CreateMap<CreatePatient, Patient>()
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Therapies, opt => opt.Ignore());
            CreateMap<UpdatePatient, Patient>();
            CreateMap<Patient, PatientResponse>();
            //.ForMember(p=>p.DoctorName,opt=>opt.MapFrom(src=>src.Do)
             

            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();
            CreateMap<User, UserResponse>();

            CreateMap<CreatePayment, Payment>();
            CreateMap<UpdatePayment, Payment>();
            CreateMap<Payment, PaymentResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Therapy.Patient.Name))
                .ForMember(dest => dest.PatientSurname, opt => opt.MapFrom(src => src.Therapy.Patient.Surname));



            CreateMap<CreateDoctor, Doctor>();
            CreateMap<UpdateDoctor, Doctor>();
            CreateMap<Doctor, DoctorResponse>();



            CreateMap<CreateTherapy, Therapy>();
            //CreateMap<UpdateTherapy, Therapy>();
            CreateMap<Therapy, TherapyResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.Name))
                .ForMember(dest => dest.PatientSurname, opt => opt.MapFrom(src => src.Patient.Surname))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name))
                .ForMember(dest => dest.DoctorSurname, opt => opt.MapFrom(src => src.Doctor.Surname))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));
               
                

        }
    }
}
