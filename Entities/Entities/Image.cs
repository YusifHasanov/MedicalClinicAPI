using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Image:BaseEntity
    {

        public string ImageData { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
