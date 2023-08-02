using Business.Abstract;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Services.Validations;
using System.Net;
using Entities.Dto.Request;

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

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            try
            {
                var response = _unitOfWorkService.PaymentService.GetAll().ToList();
                return Ok(response);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("byDateInterval")]
        public IActionResult GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            try
            {
                return Ok(_unitOfWorkService.PaymentService.GetPaymentsByDateInterval(interval).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/patient/{id:int}")]
        public IActionResult GetAllPaymentsByPatientId(int id)
        {
            try
            {
                var response = _unitOfWorkService.PaymentService.GetPaymentsByPatientId(id);
                return Ok(response);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
         
        [HttpPost]
        public async Task<IActionResult> AddPayment(CreatePayment createPayment)
        {
            try
            {
                var response = await _unitOfWorkService.PaymentService.AddAsync(createPayment);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePayment updatePayment)
        {
            try
            {
                var response = await _unitOfWorkService.PaymentService.UpdateAsync(id, updatePayment);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
