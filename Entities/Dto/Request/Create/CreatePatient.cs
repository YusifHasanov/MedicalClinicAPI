using Core.Entities;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreatePatient:BaseDto
    {

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

        public ICollection<int> PhoneNumbers { get; set; }
        public ICollection<string> Images { get; set; }
        public ICollection<CreateTherapy> Therapies { get; set; }
 

    }
}
