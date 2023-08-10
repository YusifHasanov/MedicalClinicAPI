using Entities.Entities;
using Core.Utils.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Entities.Dto;

namespace DataAccess.Concrete
{
    public static class Configuration
    {
        public static void AddConfiguration(this ModelBuilder modelBuilder)
        {
            #region Patient

            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
              .HasMany(p => p.Payments)
              .WithOne(i => i.Patient)
              .HasForeignKey(i => i.PatientId)
              .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Patient>()
                .Property(p => p.Name).HasMaxLength(55);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Surname).HasMaxLength(55);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Surname)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Patient>()
                  .Property(p => p.TotalAmount)
                  .HasAnnotation("MinValue", 0)
                  .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Patient>()
                .Ignore(p => p.Doctor);

 

            #endregion

            #region Image

            modelBuilder.Entity<Image>()
                .Property(i => i.ImageData)
                .HasColumnType("varbinary(max)");
            #endregion 

            #region Payment

            modelBuilder.Entity<Payment>()
               .Property(p => p.Amount)
               .HasAnnotation("MinValue", 0)
               .HasColumnType("decimal(18, 2)");
            //modelBuilder.Entity<Payment>()
            //    .Ignore(payment => payment.Patient);
            #endregion 

            #region User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName).IsRequired(true);

            modelBuilder.Entity<User>()
                .Property(u => u.Password).IsRequired(true);

            modelBuilder.Entity<User>()
                .Property(u => u.Role).HasDefaultValue(Role.Recepsionist);
            #endregion

            #region Doctor
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Name).IsRequired(true);

            modelBuilder.Entity<Doctor>()
                .Property(d => d.Surname).IsRequired(true);

            modelBuilder.Entity<Doctor>()
                .Ignore(doctor => doctor.Patients);

            modelBuilder.Entity<Doctor>()
          .HasMany(p => p.Patients)
          .WithOne(i => i.Doctor)
          .HasForeignKey(i => i.DoctorId)
          .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
