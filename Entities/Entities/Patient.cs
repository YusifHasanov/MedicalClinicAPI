using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Patient : BaseEntity
    {
        public Patient()
        {
            Images = new HashSet<Image>();
            Payments = new HashSet<Payment>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            DoctorPatients = new HashSet<DoctorPatient>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Diagnosis { get; set; }
        public string GeneralStateOfHealth { get; set; }
        public string DrugAllergy { get; set; }
        public string ReactionToAnesthesia { get; set; }
        public string DelayedSurgeries { get; set; }
        public Gender Gender { get; set; }
        public PregnancyStatus PregnancyStatus { get; set; }
        public string InjuryProblem { get; set; }
        public DateTime ArrivalDate { get; set; } 
        public DateTime BirthDate { get; set; }
        public string Bleeding { get; set; }
        public decimal TotalAmount { get; set; }
        public string WorkToBeDone { get; set; }
        public string WorkDone { get; set; }
        public IsCame IsCame { get; set; } 
        public ICollection<DoctorPatient> DoctorPatients { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Payment> Payments { get; set; }


    }
}
