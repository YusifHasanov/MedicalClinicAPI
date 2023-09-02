using Business.Abstract;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Services.Validations;
using System.Net;
using Entities.Dto.Request;
using Microsoft.EntityFrameworkCore;
using Core.Utils.Exceptions;
using Core.Entities;
using Entities.Dto.Response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public PaymentController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost("byDateInterval")]
        public async Task<IActionResult> GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            return Ok(await (await _unitOfWorkService.PaymentService.GetPaymentsByDateInterval(interval)).ToListAsync());
        }

        [HttpGet("byPatientId/{id:int}")]
        public IActionResult GetAllPaymentsByPatientId(int id)
        {
            var response = _unitOfWorkService.PaymentService.GetPaymentsByPatientId(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(CreatePayment createPayment)
        {
            var response = await _unitOfWorkService.PaymentService.AddAsync(createPayment);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePayment updatePayment)
        {
            var response = await _unitOfWorkService.PaymentService.UpdateAsync(id, updatePayment);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var response = await _unitOfWorkService.PaymentService.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent, response);
        }
    }
}
