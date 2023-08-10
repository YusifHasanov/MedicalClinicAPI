using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Patient:BaseEntity
    {
        public Patient() 
        {
            Images = new HashSet<Image>();
            Payments = new HashSet<Payment>();
        }
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string Diagnosis { get; set; } 
        public DateTime ArrivalDate { get; set; } 
        public decimal TotalAmount { get; set; } 
        public string Works { get; set; } 
        public bool IsCame { get; set; }
        public ICollection<Image> Images { get; set; }  
        public ICollection<Payment> Payments { get; set; } 
        public int DoctorId { get; set; }
 
        public Doctor Doctor { get; set; }
        //unvan
        //telefon nomresi

    }
}
