using Entities.Entities;
using Core.Utils.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure;
using Entities.Entities.Enums;

namespace DataAccess.Concrete
{
    public static class Configuration
    {
        public static void AddConfiguration(this ModelBuilder modelBuilder)
        {
            #region Patient

            modelBuilder.Entity<Patient>()
                .Property(p => p.Name).HasMaxLength(55);
            

            modelBuilder.Entity<Patient>()
                .Property(p => p.Surname).HasMaxLength(55);


            modelBuilder.Entity<Patient>()
                .Property(p => p.Address).HasMaxLength(1000)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Diagnosis).HasMaxLength(1000)
                .IsRequired(false);


            modelBuilder.Entity<Patient>()
                .Property(p => p.GeneralStateOfHealth).HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .Property(p => p.DrugAllergy).HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .Property(p => p.ReactionToAnesthesia).HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .Property(p => p.InjuryProblem).HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .Property(p => p.DelayedSurgeries).HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Bleeding).HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                .HasColumnType("tinyint");

            modelBuilder.Entity<Patient>()
                .Property(p => p.PregnancyStatus)
                .HasColumnType("tinyint");

        modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.PhoneNumbers)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
              .HasMany(p => p.Therapies)
              .WithOne(i => i.Patient)
              .HasForeignKey(i => i.PatientId)
              .OnDelete(DeleteBehavior.Cascade);





            #endregion

            #region Image

            modelBuilder.Entity<Image>()
                .Property(i => i.ImageData)
                .HasColumnType("varbinary(max)");
            #endregion 

            #region Payment

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentAmount)
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
                .Property(u=>u.Role)
                .HasColumnType("tinyint");  

            #endregion

            #region Doctor
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Name).IsRequired(true);

            modelBuilder.Entity<Doctor>()
                .Property(d => d.Surname).IsRequired(true);
            modelBuilder.Entity<Doctor>()
                .HasMany(doctor=> doctor.Therapies)
                .WithOne(i => i.Doctor)
                .HasForeignKey(i => i.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Doctor>()
            //    .Ignore(doctor => doctor.Patients);

            #endregion

            #region Therapy
            modelBuilder.Entity<Therapy>()
                .Property(p => p.PaymentAmount)
                .HasAnnotation("MinValue", 0)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Therapy>()
                .HasMany(p => p.Payments)
                .WithOne(i => i.Therapy)
                .HasForeignKey(i => i.TherapyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Therapy>()
                .Property(p => p.IsCame)
                .HasColumnType("tinyint");

            modelBuilder.Entity<Therapy>()
             .Property(p => p.WorkToBeDone).HasMaxLength(2500)
             .IsRequired(false);

            modelBuilder.Entity<Therapy>()
                .Property(p => p.WorkDone).HasMaxLength(2500)
                .IsRequired(false);
            #endregion
        }
    }
}
