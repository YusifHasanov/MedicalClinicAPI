using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Abstract;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess.Abstract;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DoctorService : BaseService<Doctor, UpdateDoctor, CreateDoctor, DoctorResponse>, IDoctorService
    {
        public DoctorService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals) : base(unitOfWorkRepository, mapper, logService, globals)
        {
        }

        public override async Task<Doctor> AddAsync(CreateDoctor entity)
        {
            try
            {
                var dbDoctor = _unitOfWorkRepository.DoctorRepository
                    .GetOne(doctor => doctor.Name.Equals(entity.Name) && doctor.Surname.Equals(entity.Surname));

                if (dbDoctor != null)
                    throw new Exception($"Doctor is exist with name= {entity.Name} and surname = {entity.Surname}");

                var newDoctor = _mapper.Map<Doctor>(entity);

                await _unitOfWorkRepository.DoctorRepository.AddAsync(newDoctor);
                await SaveChangesAsync();

                _logService.Log($"Doctor succesfully added with name = {newDoctor.Name} and username = {newDoctor.Surname}");

                return newDoctor;

            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public async override Task<Doctor> DeleteAsync(int id)
        {
            try
            {
                var exist = IsExist(id);
                _unitOfWorkRepository.DoctorRepository.Delete(id);
                await SaveChangesAsync();
                _logService.Log($"Doctor Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override IQueryable<DoctorResponse> GetAll()
        {
            try
            {
                var allDoctors = _unitOfWorkRepository.DoctorRepository.GetAll(true)
                    .Include(doctor => doctor.DoctorPatients)
                    .ThenInclude(patients=>patients.Patient)
                    .ThenInclude(patient => patient.Payments)
                    .ProjectTo<DoctorResponse>(_mapper.ConfigurationProvider);

                _logService.Log($"All Dcotors Selected");
                return allDoctors;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override DoctorResponse GetById(int id)
        {
            try
            {
                _ = IsExist(id);
                var result= _unitOfWorkRepository.DoctorRepository.GetDoctorWithPatientsPayments(id);
                var response = _mapper.Map<DoctorResponse>(result);
                _logService.Log($"Select Dcotor byId = {id}");
                return response;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override Doctor IsExist(int id)
        {
            var doctor = _unitOfWorkRepository.DoctorRepository.GetById(id);
            return doctor ?? throw new NotFoundException($"Doctor not found with id = {id}");
        }

        public override async Task SaveChangesAsync()
        {
            try
            {
                await _unitOfWorkRepository.DoctorRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public async override Task<Doctor> UpdateAsync(int id, UpdateDoctor entity)
        {
            try
            {
                var existDcotor = IsExist(id);
                entity.Id = id;

                var doctor = _mapper.Map(entity, existDcotor);

                _unitOfWorkRepository.DoctorRepository.Update(doctor);

                await SaveChangesAsync();

                _logService.Log($"Dcotor updated with id = {id}");

                return doctor;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }
    }
}
