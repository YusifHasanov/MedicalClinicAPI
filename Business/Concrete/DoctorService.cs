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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        public DoctorService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
        }

        public async Task<Doctor> AddAsync(CreateDoctor entity)
        {
            var dbDoctor = await _unitOfWorkRepository.DoctorRepository
                .GetOneAsync(doctor => doctor.Name.Equals(entity.Name) && doctor.Surname.Equals(entity.Surname));

            if (dbDoctor != null)
                throw new Exception($"Doctor is exist with name= {entity.Name} and surname = {entity.Surname}");

            var newDoctor = _mapper.Map<Doctor>(entity);

            await _unitOfWorkRepository.DoctorRepository.AddAsync(newDoctor);
            await _unitOfWorkRepository.SaveChangesAsync();

            return newDoctor;
        }

        public async Task<Doctor> DeleteAsync(int id)
        {
            var exist = await _unitOfWorkRepository.DoctorRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Doctor not found with id = {id}");
            _unitOfWorkRepository.DoctorRepository.Delete(id);
            await _unitOfWorkRepository.SaveChangesAsync();
            return exist;
        }

        public IQueryable<DoctorResponse> GetAll()
        {
            return _unitOfWorkRepository.DoctorRepository.GetAll()
                .ProjectTo<DoctorResponse>(_mapper.ConfigurationProvider);
        }

        public async Task<Doctor> UpdateAsync(int id, UpdateDoctor entity)
        {

            var existDcotor = await _unitOfWorkRepository.DoctorRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Doctor not found with id = {id}");
            entity.Id = id;

            var doctor = _mapper.Map(entity, existDcotor);

            _unitOfWorkRepository.DoctorRepository.Update(doctor);

            await _unitOfWorkRepository.SaveChangesAsync();

            return doctor;
        }
    }
}
