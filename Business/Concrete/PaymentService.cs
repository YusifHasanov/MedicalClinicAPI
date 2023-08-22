using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Abstract;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
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
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentService : BaseService<Payment, UpdatePayment, CreatePayment, PaymentResponse>, IPaymentService
    {
        public PaymentService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals, IHttpContextAccessor httpContextAccessor) : base(unitOfWorkRepository, mapper, logService, globals, httpContextAccessor)
        {
        }

        public override async Task<Payment> AddAsync(CreatePayment entity)
        {
            try
            {
                Therapy therapy =await _unitOfWorkRepository.TherapyRepository.GetByIdAsync(entity.TherapyId)
                    ?? throw new NotFoundException($"Theraphy don't exist with Id = {entity.TherapyId}");

                List<Payment> allPayments = _unitOfWorkRepository.PaymentRepository
                   .GetAll(payment => payment.TherapyId == therapy.Id).ToList();

                decimal sumOfAmount = allPayments.Sum(payment => payment.PaymentAmount) + entity.PaymentAmount;

                if (therapy.PaymentAmount < sumOfAmount)
                {
                    throw new Exception("Patients  payment sum can't be greater than total Amount!");
                }

                Payment newPayment = _mapper.Map<Payment>(entity);
                await _unitOfWorkRepository.PaymentRepository.AddAsync(newPayment);
                await SaveChangesAsync();

                await _logService.InfoAsync($"Add new payment AddAsync");
                return new();
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Paymentservice.cs");
                throw;
            }
        }

        public async override Task<Payment> DeleteAsync(int id)
        {
            try
            {
                var dbPayment =await IsExistAsync(id);
                _unitOfWorkRepository.PaymentRepository.Delete(id);
                await SaveChangesAsync();
               await _logService.InfoAsync($"Paymnet Deleted With id {id}");
                return dbPayment;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Paymentservice.cs DeleteAsync");
                throw;
            }
        }

        public async override Task<IQueryable<PaymentResponse>> GetAll()
        {
            try
            {
                var result = _unitOfWorkRepository.PaymentRepository.GetAll()
                    .Include(payment => payment.Therapy)
                    .ThenInclude(Therapy => Therapy.Patient)
                    .ProjectTo<PaymentResponse>(_mapper.ConfigurationProvider);

                await _logService.InfoAsync($"All Payments Selected");

                return result;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async override Task<PaymentResponse> GetById(int id)
        {
            try
            {
              
               _ = await IsExistAsync(id);
                await _logService.InfoAsync($"Select Payment byId = {id}");
                var payment = await _unitOfWorkRepository.PaymentRepository.GetByIdAsync(id);
                var paymentResponse = _mapper.Map<PaymentResponse>(payment);
                return paymentResponse;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Paymentservice.cs GetById");
                throw;
            }
        }

        public async Task<IQueryable<PaymentResponse>> GetPaymentsByDateInterval(DateIntervalRequest interval)
        {
            try
            {
                 
                string auth = _httpContextAccessor.HttpContext.Request.Headers["Token"];

                bool isNull = interval.ToDate == null || interval.FromDate == null;
                var fromDate = interval.FromDate.Value.Date;
                var toDate = interval.ToDate.Value.Date.AddDays(1);

                IQueryable<PaymentResponse> payments = isNull switch
                {
                    false => _unitOfWorkRepository.PaymentRepository.GetAll(payment =>
                     payment.PaymentDate.Date >= fromDate && payment.PaymentDate.Date < toDate)
                    .Include(payment => payment.Therapy)
                    .ThenInclude(theraphy => theraphy.Doctor)
                    .ProjectTo<PaymentResponse>(_mapper.ConfigurationProvider),

                    true => _unitOfWorkRepository.PaymentRepository.GetAll()
                    .Include(payment => payment.Therapy)
                    .ThenInclude(theraphy => theraphy.Patient)
                    .ProjectTo<PaymentResponse>(_mapper.ConfigurationProvider),

                };

                await _logService.InfoAsync(isNull
                    ? "All Payments selected."
                    : $"Payments selected where PaymentDate greater than {interval?.FromDate} and less than {interval?.ToDate}");

                return payments;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Paymentservice.cs GetPaymentsByDateInterval");
                throw;
            }
        }

        public async Task<IQueryable<PaymentResponse>> GetPaymentsByPatientId(int patientId)
        {
            try
            {
                IQueryable<PaymentResponse> payments = _unitOfWorkRepository.PaymentRepository
                        .GetAll(payment => payment.Therapy.PatientId == patientId)
                        .Include(payment => payment.Therapy)
                        .ThenInclude(theraphy => theraphy.Doctor)
                        .ProjectTo<PaymentResponse>(_mapper.ConfigurationProvider);

               await _logService.InfoAsync($"All payments selected where patientId = {patientId}");
                return payments;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :173 && Paymentservice.cs");
                throw;
            }
        }

        public override async Task<Payment> IsExistAsync(int id)
        {
            var payment = await _unitOfWorkRepository.PaymentRepository.GetByIdAsync(id);
            return payment ?? throw new NotFoundException($"Payment not found with id = {id}");
        }

        public async override Task SaveChangesAsync()
        {
            await _unitOfWorkRepository.PaymentRepository.SaveChangesAsync();
        }

        public async override Task<Payment> UpdateAsync(int id, UpdatePayment entity)
        {
            //logic deyismelidi
            try
            {
                Payment dbPayment = await IsExistAsync(id);
                Therapy therapy =await _unitOfWorkRepository.TherapyRepository.GetOneAsync(therapy => therapy.Id == entity.TherapyId)
                     ?? throw new Exception($"Therapy don't exist with Id = {entity.TherapyId}");

                List<Payment> allPayments = _unitOfWorkRepository.PaymentRepository
                   .GetAll(payment => payment.Therapy.PatientId == therapy.PatientId).ToList();

                foreach (var foundPayment in allPayments.Where(payment => payment.Id == entity.Id))
                {
                    foundPayment.PaymentAmount = entity.PaymentAmount;
                }

                decimal sumOfAmount = allPayments.Sum(payment => payment.PaymentAmount);

                //if (patient.TotalAmount < sumOfAmount)
                //{
                //    throw new Exception("Patients  payment sum can't be greater than total Amount!");
                //}

                entity.Id = id;
                var payment = _mapper.Map(entity, dbPayment);

                _unitOfWorkRepository.PaymentRepository.Update(payment);
                await SaveChangesAsync();
                return payment;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Paymentservice.cs UpdateAsync");
                throw;
            }
        }
    }
}
