using Azure.Core;
using Business.Abstract;
using Business.Concrete;
using DataAccess;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Services.BackgroundServices
{
    public class MonthlyTruncateLogTable : BackgroundService
    {
       
        private Timer? _timer = null;
        public IConfiguration Configuration { get; }
        public MonthlyTruncateLogTable(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(7));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
   

            try
            {

                string connectionString = Configuration["ConnectionStrings:MSSQL"] ?? "";

                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open(); 
                
                using SqlCommand command = new SqlCommand("TRUNCATE TABLE Logs", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch 
            { 
            }

            try
            {
                using StreamWriter writer = File.AppendText("log.txt");
                writer.WriteLine($"{DateTime.Now}: dsadasdasdasd");
            }
            catch 
            { 
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            await base.StopAsync(stoppingToken);
        }
    }




}
