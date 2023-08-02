using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Update
{
    public class UpdatePayment: BaseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int PatientId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
