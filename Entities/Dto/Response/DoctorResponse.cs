using Core.Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class DoctorResponse:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } 
         
    }
}
