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
using Microsoft.Extensions.Logging;

namespace Business.Concrete
{
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly ILogService _logService; 
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public PatientService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService )
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _logService = logService; 
        }

        public async  Task<Patient> AddAsync(CreatePatient entity)
        { 
                var newPatient = _mapper.Map<Patient>(entity);

                //await _unitOfWorkRepository.SaveChangesAsync();
                if (entity.Therapies is not null && entity.Therapies.Any())
                {
                    foreach (var therapy in entity.Therapies)
                    {
                        therapy.PatientId = newPatient.Id;
                        newPatient.Therapies.Add(_mapper.Map<Therapy>(therapy));
                    }
                }
                if (entity.Images?.Count >= 0)
                {

                    foreach (var image in entity.Images)
                    {
                        CreateImage createImage = new()
                        {
                            ImageData = image,
                            ImageDate = DateTime.Now,
                            PatientId = newPatient.Id
                        };
                        newPatient.Images.Add(_mapper.Map<Image>(createImage));
                        //var newImage   = _mapper.Map<Image>(createImage);


                        //await _unitOfWorkRepository.ImageRepository.AddAsync(newImage);
                    }



                }
                if (entity.PhoneNumbers?.Count > 0)
                {
                    foreach (var phoneNumber in entity.PhoneNumbers)
                    {
                        CreatePhoneNumber phone = new()
                        {
                            PatientId = newPatient.Id,
                            Number = phoneNumber,
                        };
                        newPatient.PhoneNumbers.Add(_mapper.Map<PhoneNumber>(phone));
                        //var newPhoneNumebr=  _mapper.Map<PhoneNumber>(phone);
                        //await _unitOfWorkRepository.PhoneNumberRepository.AddAsync(newPhoneNumebr);
                    }
                }
                await _unitOfWorkRepository.PatientRepository.AddAsync(newPatient);
                await _unitOfWorkRepository.SaveChangesAsync();
                return newPatient;
    
        }

        public async  Task<Patient> DeleteAsync(int id)
        {
 
                var exist = await _unitOfWorkRepository.PatientRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Patient not found with id = {id}");

                _unitOfWorkRepository.PatientRepository.Delete(id);
                await _unitOfWorkRepository.SaveChangesAsync();

                await _logService.InfoAsync($"Patient Deleted With id {id}");

                return exist;
   
        }
        public async  Task<PatientResponse> GetByIdAsync(int id)
        {
            var patient = await _unitOfWorkRepository.PatientRepository
            .GetByIdAsync(id) ?? throw new NotFoundException($"Patient not found with id = {id}");

            return _mapper.Map<PatientResponse>(patient); 

        }

        public IQueryable<PatientResponse> GetPatientsByDate(DateTime date)
        {
            var patients = _unitOfWorkRepository.PatientRepository
                .GetAll(p => p.ArrivalDate.Date.Equals(date.Date))
                .Include(patient => patient.Images)
                .ProjectTo<PatientResponse>(_mapper.ConfigurationProvider);

            return patients;

        }

        public async Task<IQueryable<PatientResponse>> GetPatientsByDateInterval(DateIntervalRequest interval)
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

            await _logService.InfoAsync(isNull
                ? "All Patients selected."
                : $"Patients selected where PaymentDate greater than {interval?.FromDate} and less than {interval?.ToDate}");


            return patients;


        }
        public  async Task<Patient> UpdateAsync(int id, UpdatePatient entity)
        {
            var exist = await _unitOfWorkRepository.PatientRepository
                .GetByIdAsync(id) ?? throw new NotFoundException($"Patient not found with id = {id}");

            entity.Id = id;

            var patient = _mapper.Map(entity, exist);

            _unitOfWorkRepository.PatientRepository.Update(patient);
            await _unitOfWorkRepository.SaveChangesAsync();

            return patient;

        }
    }
}
