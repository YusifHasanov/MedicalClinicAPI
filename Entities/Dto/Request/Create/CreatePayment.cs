using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreatePayment:BaseDto
    {
        public decimal Amount { get; set; }
        public int PatientId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
