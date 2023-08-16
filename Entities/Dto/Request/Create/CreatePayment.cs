using Core.Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreatePayment:BaseDto
    {
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TherapyId { get; set; }
    }
}
