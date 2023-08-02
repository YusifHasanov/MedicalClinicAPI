using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    { 
        public UnitOfWorkRepository(
            IImageRepository imageRepository,
            IPatientRepository patientRepository,
            IUserRepository userRepository,
            IPaymentRepository paymentRepository,
            IDoctorRepository doctorRepository
            )
        {
            UserRepository = userRepository;
            ImageRepository = imageRepository;
            PatientRepository = patientRepository;
            PaymentRepository = paymentRepository;
            DoctorRepository = doctorRepository;
        }




 
        public IPatientRepository PatientRepository { get; }

        public IImageRepository ImageRepository { get; }

        public IUserRepository UserRepository { get; }
        public IPaymentRepository PaymentRepository { get; }

        public IDoctorRepository DoctorRepository { get; }
    }
}
