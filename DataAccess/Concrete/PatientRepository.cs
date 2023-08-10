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
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DbEntity dbEntity) : base(dbEntity)
        {
        }

        public Patient GetPatientWithImage(int id) =>
            dbEntity.Patients.Include(patien => patien.Images)
                .FirstOrDefault(patient => patient.Id == id);
         
    }
}
