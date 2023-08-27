using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPatientService  
    {
        public IQueryable<PatientResponse> GetPatientsByDate(DateTime date);
        public Task<IQueryable<PatientResponse>> GetPatientsByDateInterval(DateIntervalRequest interval);
        public IQueryable<PatientResponse> GetAll();
        public Task<PatientResponse> GetByIdAsync(int id);
        public Task<Patient> DeleteAsync(int id);
        public Task<Patient> UpdateAsync(int id, UpdatePatient entity);
        public Task<Patient> AddAsync(CreatePatient entity);
    }
}
