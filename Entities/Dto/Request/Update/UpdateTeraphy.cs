using Core.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Update
{
    public class UpdateTeraphy:BaseDto
    {
        public int Id { get; set; }
        public DateTime TherapyDate { get; set; }
        public string? WorkToBeDone { get; set; }
        public string? WorkDone { get; set; }
        public IsCame IsCame { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PatientId { get; set; }
    }
}
