using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Doctor : BaseEntity
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}
