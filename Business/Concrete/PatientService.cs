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
    public class PatientService : BaseService<Patient, UpdatePatient, CreatePatient, PatientResponse>, IPatientService
    {

        public PatientService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals, IHttpContextAccessor httpContextAccessor) : base(unitOfWorkRepository, mapper, logService, globals, httpContextAccessor)
        {
            ;
        }

        public async override Task<Patient> AddAsync(CreatePatient entity)
        {
            try
            {

                var newPatient = _mapper.Map<Patient>(entity);

                //await SaveChangesAsync();
                await _logService.InfoAsync($"New Patient Added With id {newPatient.Id}");
                if(entity.Therapies is not null && entity.Therapies.Any() )
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
                    await _logService.InfoAsync($"Patients with id = {newPatient.Id} images added succesfully");

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
                await SaveChangesAsync();
                return newPatient;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "PatientService.cs AddAsync");
                throw;
            }
        }

        public async override Task<Patient> DeleteAsync(int id)
        {
            try
            {
                var exist = await IsExistAsync(id);

                _unitOfWorkRepository.PatientRepository.Delete(id);
                await SaveChangesAsync();

                await _logService.InfoAsync($"Patient Deleted With id {id}");

                return exist;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "PatientService.cs DeleteAsync");
                throw;
            }
        }

        public override async Task<IQueryable<PatientResponse>> GetAll()
        {
            try
            {
                var result = _unitOfWorkRepository.PatientRepository.GetAll().Include(p => p.Images)
                    .ProjectTo<PatientResponse>(_mapper.ConfigurationProvider);

                await _logService.InfoAsync($"All Patients Selected");

                return result;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :113 && PatientService.cs");
                throw;
            }
        }

        public async override Task<PatientResponse> GetById(int id)
        {
            try
            {
                _ =await   IsExistAsync(id);

                await _logService.InfoAsync($"Select Patient byId = {id}");

                var patient = _unitOfWorkRepository.PatientRepository.GetPatientWithImage(id);
                var response = _mapper.Map<PatientResponse>(patient);

                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "PatientService.cs GetById");
                throw;
            }
        }

        public async Task<IQueryable<PatientResponse>> GetPatientsByDate(DateTime date)
        {
            try
            {
                var patients = _unitOfWorkRepository.PatientRepository
                    .GetAll(p => p.ArrivalDate.Date.Equals(date.Date))
                    .Include(patient => patient.Images)
                    .ProjectTo<PatientResponse>(_mapper.ConfigurationProvider);

                await _logService.InfoAsync($"All Patients selected where ArrivalDate = {date}");
                return patients;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :152 && PatientService.cs");
                throw;
            }
        }

        public async Task<IQueryable<PatientResponse>> GetPatientsByDateInterval(DateIntervalRequest interval)
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
 
                await _logService.InfoAsync(isNull
                    ? "All Patients selected."
                    : $"Patients selected where PaymentDate greater than {interval?.FromDate} and less than {interval?.ToDate}");


                return patients;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :190 && PatientService.cs");
                throw;
            }
        }

        public async override Task<Patient> IsExistAsync(int id)
        {
            var patient =  await _unitOfWorkRepository.PatientRepository.GetByIdAsync(id);
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
                var exist = await IsExistAsync(id);
                entity.Id = id;

                var patient = _mapper.Map(entity, exist);

                _unitOfWorkRepository.PatientRepository.Update(patient);
                await SaveChangesAsync();

                await _logService.InfoAsync($"Patient updated with id = {id}");

                return patient;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "PatientService.cs UpdateAsync");
                throw;
            }

        }
    }
}
