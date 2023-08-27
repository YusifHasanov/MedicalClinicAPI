using Core.Entities;
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
    public interface IDoctorService 
    {
        public IQueryable<DoctorResponse> GetAll();
        public Task<DoctorResponse> GetByIdAsync(int id);
        public Task<Doctor> DeleteAsync(int id);
        public Task<Doctor> UpdateAsync(int id, UpdateDoctor entity);
        public Task<Doctor> AddAsync(CreateDoctor entity);
    }
}
