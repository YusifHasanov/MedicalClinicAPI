using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class ImageResponse:BaseDto
    {
        public int Id { get; set; }
        public string? ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        public int PatientId { get; set; }

        //public Patient Patient { get; set; }
    }
}
