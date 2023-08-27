using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILogService
    {
        //public void Log(string message);

        public Task InfoAsync(string message);
        public Task LogAsync(Log log);
        public Task WarnAsync(string message);      
 
        public Task ErrorAsync(Exception exception , string message);
        public Task ErrorAsync(string message);
 
    }
}
