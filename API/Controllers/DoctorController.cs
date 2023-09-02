using AutoMapper;
using Business.Abstract;
using Core.Entities;
using Core.Utils.Exceptions;
using DataAccess.Abstract;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public DoctorController(IUnitOfWorkService unitOfWorkServicer)
        {
            _unitOfWorkService = unitOfWorkServicer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWorkService.DoctorService.GetAll().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDoctor create)
        {
            var response = await _unitOfWorkService.DoctorService.AddAsync(create);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctor update)
        {
            var response = await _unitOfWorkService.DoctorService.UpdateAsync(id, update);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _unitOfWorkService.DoctorService.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent, response);
        }

    }
}
