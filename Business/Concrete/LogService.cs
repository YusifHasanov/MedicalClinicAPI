using Business.Abstract;
using DataAccess;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LogService : ILogService
    {
        private readonly DbEntity _dbEntity;
        public string FileName { get; set; } = "log.txt";



        public LogService(DbEntity dbEntity)
        {
            _dbEntity = dbEntity;
        }

        //public void Log(string message)
        //{
        //    var isExist = File.Exists(FileName);
        //    if (!isExist)
        //    {
        //        File.Create(FileName).Close();
        //    }
        //    using StreamWriter writer = File.AppendText(FileName);
        //    writer.WriteLine($"{DateTime.Now}: {message}");
        //}

        public async Task ErrorAsync(Exception exception, string message)
        {
            try
         
            {
                Log log = new()
                {
                   Message = message,
                   ExceptionStackTrace = exception.StackTrace?.Trim() ,
                   LogLevel = LogLevel.Error.ToString(),
                   LogDate = DateTime.Now,
                   ExceptionMessage = exception.Message?.Trim()
                };
                await _dbEntity.Logs.AddAsync(log);
                await _dbEntity.SaveChangesAsync();
            }
            catch
            { 
            }
        }

        public async Task ErrorAsync(string message)
        {
            try
            {
                Log log = new()
                {
                    Message = message, 
                    LogLevel = LogLevel.Error.ToString(),
                    LogDate = DateTime.Now
                };
               
                await _dbEntity.Logs.AddAsync(log);
                await _dbEntity.SaveChangesAsync();
            }
            catch
            {
            }
        }

        public async Task InfoAsync(string message)
        {
            try
            {
                Log log = new()
                {
                    Message = message, 
                    LogLevel = LogLevel.Info.ToString(),
                    LogDate = DateTime.Now
                };
                await _dbEntity.Logs.AddAsync(log);
                await _dbEntity.SaveChangesAsync();
            }
            catch
            {
            }
        }

        public async Task WarnAsync(string message)
        {
            try
            {
                Log log = new()
                {
                    Message = message,
                    ExceptionMessage = "",
                    LogLevel = LogLevel.Warning.ToString(),
                    LogDate = DateTime.Now
                };
                await _dbEntity.Logs.AddAsync(log);
                await _dbEntity.SaveChangesAsync();
            }
            catch 
            {
            }
        }
 
    }
}
