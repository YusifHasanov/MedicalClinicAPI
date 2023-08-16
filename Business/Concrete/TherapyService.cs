using AutoMapper;
using Business.Abstract;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess.Abstract;
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

namespace Business.Concrete
{
    public class TherapyService : BaseService<Therapy, UpdateTeraphy, CreateTherapy, TherapyResponse>, ITherapyService
    {
        public TherapyService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals) : base(unitOfWorkRepository, mapper, logService, globals)
        {
        }

        public async override Task<Therapy> AddAsync(CreateTherapy entity)
        {
            try
            {
                Therapy theraphy = _mapper.Map<Therapy>(entity);
               await _unitOfWorkRepository.TherapyRepository.AddAsync(theraphy);
                await  SaveChangesAsync();
                return theraphy;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public async override Task<Therapy> DeleteAsync(int id)
        {

            try
            {
                var exist = IsExist(id);
                _unitOfWorkRepository.TherapyRepository.Delete(id);
                await SaveChangesAsync();
                _logService.Log($"Patient Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override IQueryable<TherapyResponse> GetAll()
        {
            try
            {
                var all = _unitOfWorkRepository.TherapyRepository.GetAll();
                var response = _mapper.ProjectTo<TherapyResponse>(all);
                return response;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override TherapyResponse GetById(int id)
        {
            try
            {
                var therapy = IsExist(id);
                var response = _mapper.Map<TherapyResponse>(therapy);
                return response;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public IQueryable<TherapyResponse> GetTherapiesByDateInterval(DateIntervalRequest interval)
        {
            try
            {
                var all = _unitOfWorkRepository.TherapyRepository.GetAll();
                var response = _mapper.ProjectTo<TherapyResponse>(all);
                return response;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public IQueryable<TherapyResponse> GetTherapiesByPatientId(int patientId)
        {
            try
            {
                var therapies = _unitOfWorkRepository.TherapyRepository
                    .GetAll(therapy => therapy.PatientId == patientId);
                var response = _mapper.ProjectTo<TherapyResponse>(therapies);
                _logService.Log($"Get Therapies By Patient Id {patientId}");
                return response;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override Therapy IsExist(int id)
        {
           return _unitOfWorkRepository.TherapyRepository.GetById(id) ?? throw new NotFoundException("Teraphy Not Found with id" + id);
        }

        public async override Task SaveChangesAsync()
        {
           await  _unitOfWorkRepository.TherapyRepository.SaveChangesAsync();
        }

        public async override Task<Therapy> UpdateAsync(int id, UpdateTeraphy entity)
        {
            try
            {
                var exist = IsExist(id);
                entity.Id = id;
                var therapy = _mapper.Map(entity, exist);
                _unitOfWorkRepository.TherapyRepository.Update(therapy);
               await SaveChangesAsync();
                return therapy;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
