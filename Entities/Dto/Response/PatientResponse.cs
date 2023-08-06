using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class PatientResponse:BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Works { get; set; } 
        public bool IsCame { get; set; }
        public decimal PayedAmount { get; set; }
        public int DoctorId { get; set; }
    }
}
