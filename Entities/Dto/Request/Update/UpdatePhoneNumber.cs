using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Update
{
    public class UpdatePhoneNumber:BaseDto
    {
        public int Id { get; set; }

        [RegularExpression(@"^(\+[0-9]{9})$", ErrorMessage = "Telefon nömrəsnin formatı doğru deyil")]
        public int Number { get; set; }

        public int PatientId { get; set; }
    }
}
