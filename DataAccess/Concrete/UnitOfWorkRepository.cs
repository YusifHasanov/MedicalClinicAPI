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
        private readonly DbEntity dbEntity;
        public UnitOfWorkRepository(DbEntity dbEntity)
        {
            this.dbEntity = dbEntity;
        }
        public IImageRepository ImageRepository => new ImageRepository(dbEntity);

        public IPatientRepository PatientRepository =>  new PatientRepository(dbEntity);

        public IUserRepository UserRepository =>  new UserRepository(dbEntity);

        public IPaymentRepository PaymentRepository =>   new PaymentRepository(dbEntity);

        public IDoctorRepository DoctorRepository =>  new DoctorRepository(dbEntity);

        public ITherapyRepository TherapyRepository => new TherapyRepository(dbEntity);

        public INotificationRepository NotificationRepository =>  new NotificationRepository(dbEntity);

        public IPhoneNumberRepository PhoneNumberRepository => new PhoneNumberRepository(dbEntity);

        public async Task SaveChangesAsync()
        {
            await dbEntity.SaveChangesAsync();
        }
        #region Old
        //public UnitOfWorkRepository(
        //    DbEntity dbEntity,
        //    IImageRepository imageRepository,
        //    IPatientRepository patientRepository,
        //    IUserRepository userRepository,
        //    IPaymentRepository paymentRepository,
        //    IDoctorRepository doctorRepository,
        //    ITherapyRepository teraphyRepository,
        //    INotificationRepository notificationRepository,
        //    IPhoneNumberRepository phoneNumberRepository
        //    )
        //{
        //    this.dbEntity = dbEntity;

        //    UserRepository = userRepository;
        //    ImageRepository = imageRepository;
        //    PatientRepository = patientRepository;
        //    PaymentRepository = paymentRepository;
        //    DoctorRepository = doctorRepository;
        //    TherapyRepository = teraphyRepository;
        //    NotificationRepository = notificationRepository;
        //    PhoneNumberRepository = phoneNumberRepository;
        //}
        //private readonly DbEntity dbEntity;


        //public IImageRepository ImageRepository {  get; private set; }

        //public IPatientRepository PatientRepository { get; private set; }

        //public IUserRepository UserRepository { get; private set; }

        //public IPaymentRepository PaymentRepository { get; private set; }

        //public IDoctorRepository DoctorRepository { get; private set; }

        //public ITherapyRepository TherapyRepository { get; private set; }
        //public INotificationRepository NotificationRepository { get; private set; }
        //public IPhoneNumberRepository PhoneNumberRepository { get; private set; }

        #endregion



    }
}
