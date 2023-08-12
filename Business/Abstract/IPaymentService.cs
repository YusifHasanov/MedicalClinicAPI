using Core.Entities;
using Entities.Dto;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;

namespace Business.Abstract
{
    public interface IPaymentService : IService<Payment, UpdatePayment, CreatePayment, PaymentResponse>
    {
        public IQueryable<PaymentResponse> GetPaymentsByPatientId(int patientId);
        public IQueryable<PaymentResponse> GetPaymentsByDateInterval(DateIntervalRequest interval);
    }
}
