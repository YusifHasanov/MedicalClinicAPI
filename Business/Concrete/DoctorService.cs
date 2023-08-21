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
using Microsoft.AspNetCore.Http;
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
        public DoctorService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals, IHttpContextAccessor httpContextAccessor) : base(unitOfWorkRepository, mapper, logService, globals, httpContextAccessor)
        {
        }

        public override async Task<Doctor> AddAsync(CreateDoctor entity)
        {
            try
            {
                var dbDoctor =  await _unitOfWorkRepository.DoctorRepository
                    .GetOneAsync(doctor => doctor.Name.Equals(entity.Name) && doctor.Surname.Equals(entity.Surname));

                if (dbDoctor != null)
                    throw new Exception($"Doctor is exist with name= {entity.Name} and surname = {entity.Surname}");

                var newDoctor = _mapper.Map<Doctor>(entity);

                await _unitOfWorkRepository.DoctorRepository.AddAsync(newDoctor);
                await SaveChangesAsync();

                await _logService.InfoAsync($"Doctor succesfully added with name = {newDoctor.Name} and username = {newDoctor.Surname}");

                return newDoctor;

            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "DoctorService.cs AddAsync");
                throw;
            }
        }

        public async override Task<Doctor> DeleteAsync(int id)
        {
            try
            {
                var exist =  await IsExistAsync(id);
                _unitOfWorkRepository.DoctorRepository.Delete(id);
                await SaveChangesAsync();
                await _logService.InfoAsync($"Doctor Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "DoctorService.cs DeleteAsync");
                throw;
            }
        }

        public async override Task<IQueryable<DoctorResponse>> GetAll()
        {
            try
            {
                var allDoctors = _unitOfWorkRepository.DoctorRepository.GetAll()
                    .ProjectTo<DoctorResponse>(_mapper.ConfigurationProvider);

               await _logService.InfoAsync($"All Dcotors Selected");
                return allDoctors;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "DoctorService.cs GetAll");
                throw;
            }
        }

        public override async Task<DoctorResponse> GetById(int id)
        {
            try
            {
                _ =await IsExistAsync(id);
                var result =  _unitOfWorkRepository.DoctorRepository.GetDoctorWithPatientsPayments(id);
                var response = _mapper.Map<DoctorResponse>(result);

                await _logService.InfoAsync($"Select Dcotor byId = {id}");

                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "DoctorService.cs GetById");
                throw;
            }
        }

        public override async Task<Doctor> IsExistAsync(int id)
        {
            var doctor = await _unitOfWorkRepository.DoctorRepository.GetByIdAsync(id);
            return doctor ?? throw new NotFoundException($"Doctor not found with id = {id}");
        }

        public override async Task SaveChangesAsync()
        {
             
                await _unitOfWorkRepository.DoctorRepository.SaveChangesAsync();
           
        }

        public async override Task<Doctor> UpdateAsync(int id, UpdateDoctor entity)
        {
            try
            {
                var existDcotor = await IsExistAsync(id);
                entity.Id = id;

                var doctor = _mapper.Map(entity, existDcotor);

                _unitOfWorkRepository.DoctorRepository.Update(doctor);

                await SaveChangesAsync();

                await _logService.InfoAsync($"Dcotor updated with id = {id}");

                return doctor;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "DoctorService.cs UpdateAsync");
                throw;
            }
        }
    }
}
