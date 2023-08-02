using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("UnAuthorized") 
        {
        }

        public UnauthorizedException(string? message) : base(message)
        {
        }
    }
}
