using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Update
{
    public class RefreshTokenDto:BaseDto
    {
        public string AccessToken { get; set; }
    }
}