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
    public interface ITherapyService 
    {
        public Task<IQueryable<TherapyResponse>> GetTherapiesByDateInterval(DateIntervalRequest interval);
        public  IQueryable<TherapyResponse> GetTherapiesByPatientId(int patientId);
        public IQueryable<TherapyResponse> GetAll();
        public Task<TherapyResponse> GetByIdAsync(int id);
        public Task<Therapy> DeleteAsync(int id);
        public Task<Therapy> UpdateAsync(int id, UpdateTherapy entity);
        public Task<Therapy> AddAsync(CreateTherapy entity);

    }

}
