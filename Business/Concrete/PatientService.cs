using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Abstract;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PatientService : BaseService<Patient, UpdatePatient, CreatePatient, PatientResponse>, IPatientService
    {
        public PatientService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals) : base(unitOfWorkRepository, mapper, logService, globals)
        {
        }

        public async override Task<Patient> AddAsync(CreatePatient entity)
        {
            try
            {

                var newPatient = _mapper.Map<Patient>(entity);
                await _unitOfWorkRepository.PatientRepository.AddAsync(newPatient);
                await SaveChangesAsync();
                _logService.Log($"New Patient Added With id {newPatient.Id}");
                if (entity.ImageDatas?.Count >= 0)
                {
                    foreach (var image in entity.ImageDatas)
                    {
                        Image newImage = new()
                        {
                            PatientId = newPatient.Id,
                            //ImageData = System.IO.File.ReadAllBytes("D:\\Projects\\VisualStudioProjects\\ecommerce\\Server\\Entities\\house.png")
                            ImageData = image
                        };
                        await _unitOfWorkRepository.ImageRepository.AddAsync(newImage);
                    }
                    _logService.Log($"Patients with id = {newPatient.Id} images added succesfully");
                    await SaveChangesAsync();
                }

                return newPatient;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public async override Task<Patient> DeleteAsync(int id)
        {
            try
            {
                var exist = IsExist(id);
                _unitOfWorkRepository.PatientRepository.Delete(id);
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

        public override IQueryable<PatientResponse> GetAll()
        {
            try
            {
                var result = _unitOfWorkRepository.PatientRepository.GetAll().Include(p => p.Images)
                    .ProjectTo<PatientResponse>(_mapper.ConfigurationProvider);

                _logService.Log($"All Patients Selected");

                return result;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override PatientResponse GetById(int id)
        {
            try
            {
                _ = IsExist(id);

                _logService.Log($"Select Patient byId = {id}");
                var patient = _unitOfWorkRepository.PatientRepository.GetPatientWithImage(id);
                var response = _mapper.Map<PatientResponse>(patient);
                return response;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public IQueryable<PatientResponse> GetPatientsByDate(DateTime date)
        {
            try
            {
                var patients = _unitOfWorkRepository.PatientRepository
                    .GetAll(p => p.ArrivalDate.Date.Equals(date.Date))
                    .Include(patient => patient.Images)
                    .ProjectTo<PatientResponse>(_mapper.ConfigurationProvider);

                _logService.Log($"All Patients selected where ArrivalDate = {date}");
                return patients;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public IQueryable<PatientResponse> GetPatientsByDateInterval(DateIntervalRequest interval)
        {
            try
            {
                bool isNull = interval.ToDate == null || interval.FromDate == null;
                var fromDate = interval.FromDate.Value.Date;  
                var toDate = interval.ToDate.Value.Date.AddDays(1);
                IQueryable<PatientResponse> patients = isNull switch
                {
                    false => _unitOfWorkRepository.PatientRepository.GetAll(patient =>
                             patient.ArrivalDate.Date >= fromDate && patient.ArrivalDate.Date < toDate)
                            .Include(patient => patient.Images)
                            .ProjectTo<PatientResponse>(_mapper.ConfigurationProvider),

                    true => _unitOfWorkRepository.PatientRepository.GetAll().Include(p => p.Images)
                    .ProjectTo<PatientResponse>(_mapper.ConfigurationProvider),
                };

                _logService.Log(isNull
                     ? "All Patients selected."
                     : $"Patients selected where PaymentDate greater than {interval?.FromDate} and less than {interval?.ToDate}");

                return patients;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override Patient IsExist(int id)
        {
            var patient = _unitOfWorkRepository.PatientRepository.GetById(id);
            return patient ?? throw new NotFoundException($"Patient not found with id = {id}");
        }

        public override async Task SaveChangesAsync()
        {

            await _unitOfWorkRepository.PatientRepository.SaveChangesAsync();

        }

        public override async Task<Patient> UpdateAsync(int id, UpdatePatient entity)
        {
            try
            {
                var exist = IsExist(id);
                entity.Id = id;
                var patient = _mapper.Map(entity, exist);
                _unitOfWorkRepository.PatientRepository.Update(patient);
                await SaveChangesAsync();
                _logService.Log($"Patient updated with id = {id}");
                return patient;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }

        }
    }
}
