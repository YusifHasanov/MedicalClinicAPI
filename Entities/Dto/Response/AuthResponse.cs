using Core.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class AuthResponse:BaseDto
    {
        public string AccessToken { get; set; }

        public int UserId { get; set; }
        public Role Role { get; set; }

    }
}
