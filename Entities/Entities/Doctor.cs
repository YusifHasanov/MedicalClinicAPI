using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entities;
namespace Entities.Entities
{
    public class Doctor : BaseEntity
    {
        public Doctor()
        {
            DoctorPatients = new HashSet<DoctorPatient>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
 
        public ICollection<DoctorPatient> DoctorPatients { get; set; }
    }
}
