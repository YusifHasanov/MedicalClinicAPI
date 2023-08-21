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
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TherapyService : BaseService<Therapy, UpdateTherapy, CreateTherapy, TherapyResponse>, ITherapyService
    {
        public TherapyService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals, IHttpContextAccessor httpContextAccessor) : base(unitOfWorkRepository, mapper, logService, globals, httpContextAccessor)
        {
        }

        public async override Task<Therapy> AddAsync(CreateTherapy entity)
        {
            try
            {
                Therapy theraphy = _mapper.Map<Therapy>(entity);
                await _unitOfWorkRepository.TherapyRepository.AddAsync(theraphy);
                await SaveChangesAsync();
                return theraphy;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "DoctorService.cs AddAsync");
                throw;
            }
        }

        public async override Task<Therapy> DeleteAsync(int id)
        {

            try
            {
                var exist = await IsExistAsync(id);
                _unitOfWorkRepository.TherapyRepository.Delete(id);
                await SaveChangesAsync();
                await _logService.InfoAsync($"Patient Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "DoctorService.cs DeleteAsync");
                throw;
            }
        }

        public async override Task<IQueryable<TherapyResponse>> GetAll()
        {
            try
            {
                var all = _unitOfWorkRepository.TherapyRepository.GetAll();
                var response = _mapper.ProjectTo<TherapyResponse>(all);

                await _logService.InfoAsync("Select All Therapies");
                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "TherapyService.cs GetAll");
                throw;
            }
        }

        public async override Task<TherapyResponse> GetById(int id)
        {
            try
            {
                var therapy = await IsExistAsync(id);
                var response = _mapper.Map<TherapyResponse>(therapy);

                await _logService.InfoAsync($"Select Therapy byId = {id}");

                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "TherapyService.cs GetById");
                throw;
            }
        }

        public async Task<IQueryable<TherapyResponse>> GetTherapiesByDateInterval(DateIntervalRequest interval)
        {
            try
            {
                var all = _unitOfWorkRepository.TherapyRepository.GetAll();
                var response = _mapper.ProjectTo<TherapyResponse>(all);

               await _logService.InfoAsync($"Select Therapies by Date Interval {interval.FromDate} - {interval.ToDate}");

                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "TherapyService.cs GetTherapiesByDateInterval");
                throw;
            }
        }

        public async Task<IQueryable<TherapyResponse>> GetTherapiesByPatientId(int patientId)
        {
            try
            {
                var therapies = _unitOfWorkRepository.TherapyRepository
                    .GetAll(therapy => therapy.PatientId == patientId);
                var response = _mapper.ProjectTo<TherapyResponse>(therapies);
               await _logService.InfoAsync($"Get Therapies By Patient Id {patientId}");
                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "TherapyService.cs GetTherapiesByPatientId");
                throw;
            }
        }

        public async override Task<Therapy> IsExistAsync(int id)
        {
            return await _unitOfWorkRepository.TherapyRepository.GetByIdAsync(id) ?? throw new NotFoundException("Teraphy Not Found with id" + id);
        }

        public async override Task SaveChangesAsync()
        {
            await _unitOfWorkRepository.TherapyRepository.SaveChangesAsync();
        }

        public async override Task<Therapy> UpdateAsync(int id, UpdateTherapy entity)
        {
            try
            {
                var exist = await IsExistAsync(id);
                entity.Id = id;
                var therapy = _mapper.Map(entity, exist);
                _unitOfWorkRepository.TherapyRepository.Update(therapy);

                await _logService.InfoAsync($"Therapy Updated with id {id}");

                await SaveChangesAsync();
                return therapy;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "TherapyService.cs UpdateAsync");
                throw;
            }
        }
    }
}
