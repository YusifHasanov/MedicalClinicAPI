using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        public string? Message { get; set; }
        public int? ResponseCode { get; set; }
        public DateTime LogDate { get; set; }
        public string LogLevel { get; set; }
    }
}
