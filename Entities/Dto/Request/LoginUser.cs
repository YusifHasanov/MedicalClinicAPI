using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request
{
    public class LoginUser:IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        //public Role Role { get; set; }
    }
}
