using Core.Entities;
using Entities.Entities.Enums;
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
            Therapies = new HashSet<Therapy>();
            PhoneNumbers = new HashSet<PhoneNumber>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Adress { get; set; }
        public string Diagnosis { get; set; }
        public string? GeneralStateOfHealth { get; set; }
        public string? DrugAllergy { get; set; }
        public string? ReactionToAnesthesia { get; set; }
        public string? DelayedSurgeries { get; set; }
        public Gender Gender { get; set; }
        public PregnancyStatus PregnancyStatus { get; set; }
        public string? InjuryProblem { get; set; }
        public DateTime ArrivalDate { get; set; } 
        public DateTime? BirthDate { get; set; }
        public string? Bleeding { get; set; }
  
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Therapy> Therapies { get; set; }


    }
}
