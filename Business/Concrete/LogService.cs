using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LogService: ILogService
    {
        public string FileName { get; set; } = "log.txt";
        public void Log(string message)
        {
            IsFileExist(FileName);
            using StreamWriter writer = File.AppendText(FileName);
            writer.WriteLine($"{DateTime.Now}: {message}");
        }

        private void IsFileExist(string FileName)
        {
            var isExist = File.Exists(FileName);
            if (!isExist)
            {
                File.Create(FileName).Close();
            }
        }
    }
}
