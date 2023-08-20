using Core.Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class PaymentResponse:BaseDto
    {
        public int Id { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TherapyId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
     
        //public Patient Patient { get; set; }
    }
}
