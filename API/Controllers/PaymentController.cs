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

                return Ok(await  _unitOfWorkService.PaymentService.GetAll().ToListAsync());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { StatusCode = (int)HttpStatusCode.NotFound, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }


        [HttpPost("byDateInterval")]
        public async Task<IActionResult> GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            try
            {
                return Ok(await (await _unitOfWorkService.PaymentService.GetPaymentsByDateInterval(interval)).ToListAsync());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { StatusCode = (int)HttpStatusCode.NotFound, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpGet("byPatientId/{id:int}")]
        public async Task<IActionResult> GetAllPaymentsByPatientId(int id)
        {
            try
            {
                var response = await _unitOfWorkService.PaymentService.GetPaymentsByPatientId(id);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { StatusCode = (int)HttpStatusCode.NotFound, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
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
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { StatusCode = (int)HttpStatusCode.NotFound, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
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
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { StatusCode = (int)HttpStatusCode.NotFound, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var response = await _unitOfWorkService.PaymentService.DeleteAsync(id);
                return StatusCode((int)HttpStatusCode.NoContent, response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { StatusCode = (int)HttpStatusCode.NotFound, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { StatusCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }
    }
}
