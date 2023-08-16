using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreateImage:BaseDto
    {
        public DateTime ImageDate { get; set; }
        public string ImageData { get; set; }

        public int PatientId { get; set; }
    }
}
