using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Entities.Entities.Enums;

namespace Entities.Dto.Response
{
    public class UserResponse:BaseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
