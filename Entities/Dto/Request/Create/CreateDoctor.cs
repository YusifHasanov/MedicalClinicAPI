using Core.Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreateDoctor:BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
