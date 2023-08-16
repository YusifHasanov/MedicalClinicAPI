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
            IDoctorRepository doctorRepository,
            ITherapyRepository teraphyRepository,
            INotificationRepository notificationRepository,
            IPhoneNumberRepository phoneNumberRepository
            )
        {
            UserRepository = userRepository;
            ImageRepository = imageRepository;
            PatientRepository = patientRepository;
            PaymentRepository = paymentRepository;
            DoctorRepository = doctorRepository;
            TherapyRepository = teraphyRepository;
            NotificationRepository = notificationRepository;
            PhoneNumberRepository = phoneNumberRepository;
        }
        //private readonly DbEntity dbEntity;

        public IImageRepository ImageRepository {  get; private set; }

        public IPatientRepository PatientRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IPaymentRepository PaymentRepository { get; private set; }

        public IDoctorRepository DoctorRepository { get; private set; }

        public ITherapyRepository TherapyRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; private set; }
        public IPhoneNumberRepository PhoneNumberRepository { get; private set; }



        //public UnitOfWorkRepository(DbEntity dbentity)
        //{
        //    this.dbEntity = dbentity;
        //}




        //public IPatientRepository PatientRepository  => new PatientRepository(dbEntity);

        //public IImageRepository ImageRepository => new ImageRepository(dbEntity);

        //public IUserRepository UserRepository   => new UserRepository(dbEntity);
        //public IPaymentRepository PaymentRepository => new PaymentRepository(dbEntity);

        //public IDoctorRepository DoctorRepository  => new DoctorRepository(dbEntity);
    }
}
