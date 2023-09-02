using Business.Abstract;
using Core.Entities;
using Core.Utils.Exceptions;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public PatientController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePatient createPatient)
        {
            var response = await _unitOfWorkService.PatientService.AddAsync(createPatient);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePatient updatePatient)
        {
            var response = await _unitOfWorkService.PatientService.UpdateAsync(id, updatePatient);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _unitOfWorkService.PatientService.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent, response);
        }


        [HttpPost("byDateInterval")]
        public async Task<IActionResult> GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            return Ok(await (await _unitOfWorkService.PatientService.GetPatientsByDateInterval(interval)).ToListAsync());
        }


        [HttpGet("{date:DateTime}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            return Ok(await _unitOfWorkService.PatientService.GetPatientsByDate(date).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _unitOfWorkService.PatientService.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
