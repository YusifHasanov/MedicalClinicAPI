using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUnitOfWorkService
    {  
        public IPatientService PatientService { get; }
        public IImageService ImageService { get; }  
        public IUserService UserService { get; }  
        public IPaymentService PaymentService { get; }  
        public IDoctorService DoctorService { get; }  
        public ITherapyService TherapyService { get; }

        public INotificationService NotificationService { get; }
    }
}
