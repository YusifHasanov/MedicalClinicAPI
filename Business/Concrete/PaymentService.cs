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
    public class PaymentService :   IPaymentService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
 
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService,   IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _logService = logService; 
            _httpContextAccessor = httpContextAccessor;


        }

        public  async Task<Payment> AddAsync(CreatePayment entity)
        {

            Therapy therapy = await _unitOfWorkRepository.TherapyRepository.GetByIdAsync(entity.TherapyId)
                ?? throw new NotFoundException($"Theraphy don't exist with Id = {entity.TherapyId}");

            decimal sumOfAmount = _unitOfWorkRepository.PaymentRepository
                .GetAll(payment => payment.TherapyId == therapy.Id)
                .Sum(payment => payment.PaymentAmount) + entity.PaymentAmount;


            if (therapy.PaymentAmount < sumOfAmount)
            {
                throw new Exception("Patients  payment sum can't be greater than total Amount!");
            }

            Payment newPayment = _mapper.Map<Payment>(entity);
            await _unitOfWorkRepository.PaymentRepository.AddAsync(newPayment);
            await _unitOfWorkRepository.SaveChangesAsync();

            return new();
        }

        public async  Task<Payment> DeleteAsync(int id)
        {

            var dbPayment = await _unitOfWorkRepository.PaymentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Payment not found with id = {id}");

            _unitOfWorkRepository.PaymentRepository.Delete(id);

            await _unitOfWorkRepository.SaveChangesAsync();

            return dbPayment;
        }
        public async   Task<IQueryable<PaymentResponse>> GetPaymentsByDateInterval(DateIntervalRequest interval)
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

        public IQueryable<PaymentResponse> GetPaymentsByPatientId(int patientId)
        {
            IQueryable<PaymentResponse> payments = _unitOfWorkRepository.PaymentRepository
                    .GetAll(payment => payment.Therapy.PatientId == patientId)
                    .Include(payment => payment.Therapy)
                    .ThenInclude(theraphy => theraphy.Doctor)
                    .ProjectTo<PaymentResponse>(_mapper.ConfigurationProvider);


            return payments;
        }


        public async  Task<Payment> UpdateAsync(int id, UpdatePayment entity)
        {
            //cahnge logic

            Payment dbPayment = await _unitOfWorkRepository.PaymentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Payment not found with id = {id}");

            Therapy therapy = await _unitOfWorkRepository.TherapyRepository.GetOneAsync(therapy => therapy.Id == entity.TherapyId)
                 ?? throw new Exception($"Therapy don't exist with Id = {entity.TherapyId}");

            IQueryable<Payment> allPayments = _unitOfWorkRepository.PaymentRepository
               .GetAll(payment => payment.Therapy.PatientId == therapy.PatientId, true);

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
            await _unitOfWorkRepository.SaveChangesAsync();
            return payment;
        }
    }
}
