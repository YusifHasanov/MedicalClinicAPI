using Business.Abstract;
using Core.Entities;
using Core.Utils.Exceptions;
using Entities.Dto.Request;
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
    public class TherapyController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public TherapyController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTherapyAsync(CreateTherapy createTherapy)
        {
            var response = await _unitOfWorkService.TherapyService.AddAsync(createTherapy);
            return Ok(response);
        }

        [HttpPost("byDateInterval")]
        public async Task<IActionResult> GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            return Ok(await (await _unitOfWorkService.TherapyService.GetTherapiesByDateInterval(interval)).ToListAsync());
        }

        [HttpGet("byPatientId/{patientId}")]
        public async Task<IActionResult> GetAllByPatientId(int patientId)
        {
            return Ok(await _unitOfWorkService.TherapyService.GetTherapiesByPatientId(patientId).ToListAsync());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTherapy(int id)
        {

            var response = await _unitOfWorkService.TherapyService.DeleteAsync(id);
            return Ok(response);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTherapy(int id, [FromBody] UpdateTherapy updateTeraphy)
        {
            var response = await _unitOfWorkService.TherapyService.UpdateAsync(id, updateTeraphy);
            return Ok(response);
        }
    }
}
