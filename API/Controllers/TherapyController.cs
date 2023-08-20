using Business.Abstract;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<IActionResult> GetAllTherapies()
        {
            try
            {
              
                return Ok(await( await _unitOfWorkService.TherapyService.GetAll()).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   

        [HttpPost]
        public async Task<IActionResult> AddTherapyAsync(CreateTherapy createTherapy)
        {
            try
            {
                var response  = await _unitOfWorkService.TherapyService.AddAsync(createTherapy);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return  BadRequest(ex.Message);
            }
        }

        [HttpPost("byDateInterval")]
        public async Task<IActionResult> GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            try
            {
                return Ok(await (await _unitOfWorkService.TherapyService.GetTherapiesByDateInterval(interval)).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byPatientId/{patientId}")]
        public async Task<IActionResult> GetAllByPatientId(int patientId)
        {
            try
            {
                return Ok(await (await _unitOfWorkService.TherapyService.GetTherapiesByPatientId(patientId)).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTherapy(int id)
        {
            try
            {
               var response =  await _unitOfWorkService.TherapyService.DeleteAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
