using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public  class Payment:BaseEntity
    {
        public decimal PaymentAmount { get; set; } 
        public DateTime PaymentDate { get; set; }
        public int TherapyId { get; set; }
        public Therapy Therapy { get; set; }
    }
}
