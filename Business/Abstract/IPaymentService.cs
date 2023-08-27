using Core.Entities;
using Entities.Dto;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;

namespace Business.Abstract
{
    public interface IPaymentService 
    {
        public Task<IQueryable<PaymentResponse>> GetPaymentsByPatientId(int patientId);
        public Task<IQueryable<PaymentResponse>> GetPaymentsByDateInterval(DateIntervalRequest interval);

        public IQueryable<PaymentResponse> GetAll();
        public Task<PaymentResponse> GetByIdAsync(int id);
        public Task<Payment> DeleteAsync(int id);
        public Task<Payment> UpdateAsync(int id, UpdatePayment entity);
        public Task<Payment> AddAsync(CreatePayment entity);
    }
}
