using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Constants
{
    public static class Contants
    { 

 
        public static string SecretKey { get;} = "E8B7CEEF9D2A9B3CFF6D51DAF955A";
        public static string Issuer { get;} = "http://localhost:5000";
        public static string Audience { get;} = "http://localhost:5000";
       
    }
}
