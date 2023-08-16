using Core.Entities;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class TherapyResponse : BaseDto
    {
        public int Id { get; set; }
        public DateTime TherapyDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string? WorkToBeDone { get; set; }
        public string? WorkDone { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public int DoctorId { get; set; }
        public IsCame IsCame { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public ICollection<PaymentResponse> Payments { get; set; }
    }
}
