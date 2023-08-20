using Business.Abstract;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Services.Validations;
using System.Net;
using Entities.Dto.Request;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
             
                return Ok(await (await _unitOfWorkService.PaymentService.GetAll()).ToListAsync());
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("byDateInterval")]
        public async Task<IActionResult> GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            try
            {
                return Ok(await (await _unitOfWorkService.PaymentService.GetPaymentsByDateInterval(interval)).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byPatientId/{id:int}")]
        public  async Task<IActionResult> GetAllPaymentsByPatientId(int id)
        {
            try
            {
                var response = await _unitOfWorkService.PaymentService.GetPaymentsByPatientId(id);
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
