using Core.Entities;
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

        public DateTime ImageDate { get; set; }
        public string ImageData { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
