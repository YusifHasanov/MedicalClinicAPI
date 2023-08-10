using DataAccess.Abstract;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DbEntity dbEntity) : base(dbEntity)
        {
        }

        public Payment GetPaymnetWithPatientAndDoctor(int id)
        { 
            return dbEntity.Payments
                    .Include(payment => payment.Patient)
                    .ThenInclude(patient => patient.Doctor)
                    .FirstOrDefault(payment => payment.Id == id);
        }
    }
}
