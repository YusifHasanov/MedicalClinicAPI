using AutoMapper;
using Business.Abstract;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentService : BaseService<Payment, UpdatePayment, CreatePayment>, IPaymentService
    {
        public PaymentService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals) : base(unitOfWorkRepository, mapper, logService, globals)
        {
        }
        
        public override async Task<Payment> AddAsync(CreatePayment entity)
        {
            try
            {
                Patient patient = _unitOfWorkRepository.PatientRepository.GetById(entity.PatientId)
                    ?? throw new Exception($"Patient don't exist with Id = {entity.PatientId}");

                List<Payment> allPayments = _unitOfWorkRepository.PaymentRepository
                   .GetAll(payment => payment.PatientId == patient.Id).ToList();

                decimal sumOfAmount = allPayments.Sum(payment => payment.Amount) + entity.Amount;

                if (patient.TotalAmount < sumOfAmount)
                {
                    throw new Exception("Patients  payment sum can't be greater than total Amount!");
                }

                Payment newPayment = _mapper.Map<Payment>(entity);
                await _unitOfWorkRepository.PaymentRepository.AddAsync(newPayment);
                await SaveChangesAsync();

                _logService.Log($"Add new payment for {patient.Name} amount is {newPayment.Amount}");
                return newPayment;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public async override Task<Payment> DeleteAsync(int id)
        {
            try
            {
                var dbPayment = IsExist(id);
                _unitOfWorkRepository.PaymentRepository.Delete(id);
                await SaveChangesAsync();
                _logService.Log($"Paymnet Deleted With id {id}");
                return dbPayment;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override IQueryable<Payment> GetAll()
        {
            try
            {
                var result = _unitOfWorkRepository.PaymentRepository.GetAll();
                _logService.Log($"All Payments Selected");
                if (result == null)
                    throw new NotFoundException("Payment not found");
                return result;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override Payment GetById(int id)
        {
            try
            {
                _logService.Log($"Select Payment byId = {id}");
                return IsExist(id);
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public IQueryable<Payment> GetPaymentsByDateInterval(DateIntervalRequest interval)
        {
            try
            {
                bool isNull = interval.ToDate == null;
                IQueryable<Payment> payments = isNull switch
                {
                    false => _unitOfWorkRepository.PaymentRepository.GetAll(payment =>
                                payment.PaymentDate >= interval.FromDate && payment.PaymentDate <= interval.ToDate),
                    true => _unitOfWorkRepository.PaymentRepository.GetAll(),
                }; 

                _logService.Log(isNull
                    ? "All Payments selected."
                    : $"Payments selected where PaymentDate greater than {interval?.FromDate} and less than {interval?.ToDate}");

                return payments;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public IQueryable<Payment> GetPaymentsByPatientId(int patientId)
        {
            try
            {
                IQueryable<Payment> payments = _unitOfWorkRepository.PaymentRepository
                    .GetAll(payment => payment.PatientId == patientId);
                _logService.Log($"All payments selected where patientId = {patientId}");
                return payments;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override Payment IsExist(int id)
        {
            var payment = _unitOfWorkRepository.PaymentRepository.GetById(id);
            return payment ?? throw new NotFoundException($"Payment not found with id = {id}");
        }

        public async override Task SaveChangesAsync()
        {
          await  _unitOfWorkRepository.PaymentRepository.SaveChangesAsync();
        }

        public async override Task<Payment> UpdateAsync(int id, UpdatePayment entity)
        {
            try
            {
                Payment dbPayment = IsExist(id);
                Patient patient = _unitOfWorkRepository.PatientRepository.GetOne(patient => patient.Id == entity.PatientId)
                     ?? throw new Exception($"Patient don't exist with Id = {entity.PatientId}");

                List<Payment> allPayments = _unitOfWorkRepository.PaymentRepository
                   .GetAll(payment => payment.PatientId == patient.Id).ToList();

                foreach (var foundPayment in allPayments.Where(payment => payment.Id == entity.Id))
                {
                    foundPayment.Amount = entity.Amount;
                }

                decimal sumOfAmount = allPayments.Sum(payment => payment.Amount) ;

                if(patient.TotalAmount < sumOfAmount)
                {
                    throw new Exception("Patients  payment sum can't be greater than total Amount!");
                }

                entity.Id = id;
                var payment = _mapper.Map(entity, dbPayment);
                  
                _unitOfWorkRepository.PaymentRepository.Update(payment);
                await SaveChangesAsync();  
                return payment;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }
    }
}
