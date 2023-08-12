using Core.DataAccess;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPaymentRepository:IRepository<Payment>
    {
        public Payment GetPaymnetWithPatientAndDoctor(int id);
    }
}
