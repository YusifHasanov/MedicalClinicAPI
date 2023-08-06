﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Response
{
    public class UserResponse:BaseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string AccessToken { get; set; }

        public Role Role { get; set; }
    }
}