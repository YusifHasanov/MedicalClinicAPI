using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreatePatient:BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Works { get; set; }
        public int DoctorId { get; set; }
        public ICollection<byte[]> ImageDatas { get; set; }
    }
}
