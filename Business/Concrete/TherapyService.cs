using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TherapyService  : ITherapyService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService; 
        public TherapyService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService )  
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _logService = logService; 
        }

        public async  Task<Therapy> AddAsync(CreateTherapy entity)
        {
            Therapy theraphy = _mapper.Map<Therapy>(entity);
            await _unitOfWorkRepository.TherapyRepository.AddAsync(theraphy);
            await _unitOfWorkRepository.SaveChangesAsync();
            return theraphy;
        }
        public async  Task<Therapy> DeleteAsync(int id)
        { 
                var exist = await _unitOfWorkRepository.TherapyRepository.GetByIdAsync(id) ?? throw new NotFoundException("Teraphy Not Found with id" + id);
                _unitOfWorkRepository.TherapyRepository.Delete(id);
                await _unitOfWorkRepository.SaveChangesAsync();

                return exist;
        }
        public   IQueryable<TherapyResponse> GetAll()
        {
   
                var all = _unitOfWorkRepository.TherapyRepository.GetAll();
                var response = _mapper.ProjectTo<TherapyResponse>(all);


                return response; 
        }
        public async  Task<TherapyResponse> GetByIdAsync(int id)
        {
 
                var therapy = await _unitOfWorkRepository.TherapyRepository.GetByIdAsync(id) ?? throw new NotFoundException("Teraphy Not Found with id" + id);
                var response = _mapper.Map<TherapyResponse>(therapy);



                return response; 
        }
        public async  Task<Therapy> UpdateAsync(int id, UpdateTherapy entity)
        {
 
                var exist = await _unitOfWorkRepository.TherapyRepository.GetByIdAsync(id) ?? throw new NotFoundException("Teraphy Not Found with id" + id);
                entity.Id = id;
                var therapy = _mapper.Map(entity, exist);
                _unitOfWorkRepository.TherapyRepository.Update(therapy);



                await _unitOfWorkRepository.SaveChangesAsync();
                return therapy; 
        }
 
        public async Task<IQueryable<TherapyResponse>> GetTherapiesByDateInterval(DateIntervalRequest interval)
        {
 
                bool isNull = interval.ToDate == null || interval.FromDate == null;
                var fromDate = interval.FromDate.Value.Date;
                var toDate = interval.ToDate.Value.Date.AddDays(1);

                IQueryable<TherapyResponse> therapies = isNull switch
                {
                    false => _unitOfWorkRepository.TherapyRepository.GetAll(therapy =>
                             therapy.TherapyDate.Date >= fromDate && therapy.TherapyDate.Date < toDate)
                            .ProjectTo<TherapyResponse>(_mapper.ConfigurationProvider),

                    true => _unitOfWorkRepository.TherapyRepository.GetAll()
                    .ProjectTo<TherapyResponse>(_mapper.ConfigurationProvider),
                };

                await _logService.InfoAsync(isNull
                    ? "All Patients selected."
                    : $"Patients selected where PaymentDate greater than {interval?.FromDate} and less than {interval?.ToDate}");

                return therapies; 
        }
        public  IQueryable<TherapyResponse> GetTherapiesByPatientId(int patientId)
        {
 
                var therapies = _unitOfWorkRepository.TherapyRepository
                    .GetAll(therapy => therapy.PatientId == patientId);
                var response = _mapper.ProjectTo<TherapyResponse>(therapies);

                return response; 
        }
    }
}
