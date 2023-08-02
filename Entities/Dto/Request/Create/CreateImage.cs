using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreateImage:BaseDto
    {
        public string? ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        public int PatientId { get; set; }
    }
}
