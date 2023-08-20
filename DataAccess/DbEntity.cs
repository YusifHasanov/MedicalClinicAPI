using Entities.Entities;
using Core.Utils.Constants;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace DataAccess
{
    public class DbEntity : DbContext
    {
        private readonly Globals _globals;
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbEntity(Globals globals) : base()
        {
            _globals = globals;
        }
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_globals.SqlServer, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }).LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
        
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            try
            {
                foreach (var data in datas)
                {
                    _ = data.State switch
                    {
                        EntityState.Modified => data.Entity.UpdateDate = DateTime.Now,
                        EntityState.Added => data.Entity.CreateDate = DateTime.Now,
                        EntityState.Unchanged => null,
                        EntityState.Detached => null,
                        EntityState.Deleted => null,
                        _ => null
                    };
                }
            }
            catch
            { 
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration();
            base.OnModelCreating(modelBuilder);
        }
    }
}