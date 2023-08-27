using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Update
{
    public class RefreshTokenDto:IDto
    {
        public string AccessToken { get; set; }
    }
}
