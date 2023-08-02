using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Constants
{
    public class Globals
    {
        private readonly IConfiguration _configuration;
        public Globals(IConfiguration configuration)
        {
            _configuration = configuration;
            SqlServer = _configuration["ConnectionStrings:MSSQL"] ?? "";
            Issuer = _configuration["JWT:Issuer"] ?? "";
            Audience = _configuration["JWT:Audience"] ?? "";
            SecretKey = _configuration["JWT:SecretKey"] ?? "";
        }
        public string SqlServer { get; }
        public string EmailRegexPattern { get; } = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
        public string PasswordRegexPattern { get; } = @"^(?=.*\\d).{8,}$";
        public string Issuer { get; }
        public string Audience { get; }
        public string SecretKey { get; }
         
    }
}
