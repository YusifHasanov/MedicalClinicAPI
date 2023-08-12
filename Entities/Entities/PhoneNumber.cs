using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class PhoneNumber :BaseEntity
    {
        public int Number { get; set; }
        
        public int PatientId { get; set; }  
        public Patient Patient { get; set; }
    }
}
