using Business.Abstract;
using Core.Entities;
using Core.Utils.Exceptions;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public NotificationController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByUserIdAsync(int id)
        {
            try
            {
                var notifications = await (await _unitOfWorkService.NotificationService.GetByUserIdAsync(id)).ToListAsync();
                return Ok(notifications);
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


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notifications = await  _unitOfWorkService.NotificationService.GetAll().ToListAsync();
                return Ok(notifications);
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
        public async Task<IActionResult> Add(CreateNotification notification)
        {
            try
            {
                var response = await _unitOfWorkService.NotificationService.AddAsync(notification);
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


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _unitOfWorkService.NotificationService.DeleteAsync(id);
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


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody]UpdateNotification notification)
        {
            try
            {
                var response = await _unitOfWorkService.NotificationService.UpdateAsync( id,notification);
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
    }
}
