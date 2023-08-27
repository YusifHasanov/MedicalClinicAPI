using Core.Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class ImageResponse:IDto
    {
        public int Id { get; set; } 
        public string ImageData { get; set; }

        public int PatientId { get; set; }

        //public Patient Patient { get; set; }
    }
}
