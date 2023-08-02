using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Payment:BaseEntity
    {
        public decimal Amount { get; set; } 
        public int PatientId { get; set; }
        public DateTime PaymentDate { get; set; }
        public Patient Patient { get; set; }
    }
}
