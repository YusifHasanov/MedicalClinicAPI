using Core.Entities;
using Core.Utils.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreatePhoneNumber:BaseDto
    {
        [RegularExpression(@"^(\+[0-9]{9})$", ErrorMessage = "Telefon nömrəsnin formatı doğru deyil")]
        public int Number { get; set; }

        public int PatientId { get; set; }  
    }
}
