using Core.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string AccessToken { get; set; }

        public Role Role { get; set; }  
    }
}
