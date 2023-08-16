using Core.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Therapy : BaseEntity
    {
        public Therapy()
        {
            Payments = new HashSet<Payment>();
        }
        public DateTime TherapyDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string? WorkToBeDone { get; set; }
        public string? WorkDone { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public IsCame IsCame { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
