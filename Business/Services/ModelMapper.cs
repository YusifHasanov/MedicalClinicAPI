using AutoMapper; 
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<CreateImage, Image>();
            CreateMap<UpdateImage, Image>();


            CreateMap<CreatePatient, Patient>();
            CreateMap<UpdatePatient, Patient>();

            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();

            CreateMap<CreatePayment, Payment>();
            CreateMap<UpdatePayment, Payment>();

            CreateMap<CreateDoctor, Doctor>();
            CreateMap<UpdateDoctor, Doctor>();

        }
    }
}
