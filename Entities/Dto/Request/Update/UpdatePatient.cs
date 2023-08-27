using Core.Entities;
using Entities.Dto.Request.Create;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Update
{
    public class UpdatePatient:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ArrivalDate { get; set; }
  
        public string Address { get; set; }
        public string GeneralStateOfHealth { get; set; }
        public string DrugAllergy { get; set; }
        public string ReactionToAnesthesia { get; set; }
        public string DelayedSurgeries { get; set; }
        public Gender Gender { get; set; }
        public PregnancyStatus PregnancyStatus { get; set; }
        public string InjuryProblem { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bleeding { get; set; }

 


    }
}
