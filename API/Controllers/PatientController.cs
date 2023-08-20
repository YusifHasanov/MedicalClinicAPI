using Business.Abstract;
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

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                

                return Ok(await (await _unitOfWorkService.PatientService.GetAll()).ToListAsync());
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
                 

                return Ok(await (await _unitOfWorkService.PatientService.GetPatientsByDateInterval(interval)).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{date:DateTime}")]
        public async Task<IActionResult> Get(DateTime date)
        {
            try
            {
              
                return Ok(await (await _unitOfWorkService.PatientService.GetPatientsByDate(date)).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _unitOfWorkService.PatientService.GetById(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePatient createPatient)
        {
            try
            {
                var response = await _unitOfWorkService.PatientService.AddAsync(createPatient);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePatient updatePatient)
        {
            try
            {
                var response = await _unitOfWorkService.PatientService.UpdateAsync(id, updatePatient);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _unitOfWorkService.PatientService.DeleteAsync(id);
                return StatusCode((int)HttpStatusCode.NoContent, response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
