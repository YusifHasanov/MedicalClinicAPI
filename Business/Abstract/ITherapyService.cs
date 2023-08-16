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
    public interface ITherapyService:IService<Therapy,UpdateTeraphy, CreateTherapy, TherapyResponse>
    {
        public IQueryable<TherapyResponse> GetTherapiesByDateInterval(DateIntervalRequest interval);
        public IQueryable<TherapyResponse> GetTherapiesByPatientId(int patientId);

    }

}
