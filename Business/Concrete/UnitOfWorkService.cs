using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        public UnitOfWorkService(
            IImageService imageService,
            IPatientService patientService,
            IUserService userService,
            IPaymentService paymentService,
            IDoctorService doctorService
            ) { 
            ImageService = imageService;
            PatientService = patientService;
            UserService = userService;
            PaymentService = paymentService;
            DoctorService = doctorService;
        } 

   
        public IImageService ImageService { get; }
        public IPatientService PatientService { get; }

        public IUserService UserService { get; }

        public IPaymentService PaymentService { get; }

        public IDoctorService DoctorService { get; }
    }
}