using DataAccess.Abstract;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DbEntity dbEntity) : base(dbEntity)
        {
        }

        public Doctor GetDoctorWithPatientsPayments(int id)
        {
            return dbEntity.Doctors
                .Include(d => d.Patients)
                .ThenInclude(patient => patient.Payments) 
                .FirstOrDefault(d => d.Id == id);
                 
        }
    }
}
