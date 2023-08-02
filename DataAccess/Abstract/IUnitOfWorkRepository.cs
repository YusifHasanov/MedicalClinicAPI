using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUnitOfWorkRepository
    {
        public IImageRepository ImageRepository { get; }
       
        public IPatientRepository PatientRepository  { get; }
        public IUserRepository UserRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
    }

}

