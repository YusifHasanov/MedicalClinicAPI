using Business.Abstract;
using Core.Utils.Exceptions;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_unitOfWorkService.PatientService.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("byDateInterval")]
        public IActionResult GetAllByDateInterval([FromBody]DateIntervalRequest interval )
        {
            try
                {
                return Ok(_unitOfWorkService.PatientService.GetPatientsByDateInterval(interval).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         

        [HttpGet("{date:DateTime}")]
        public IActionResult Get(DateTime date)
        {
            try
            { 
                return Ok(_unitOfWorkService.PatientService.GetPatientsByDate(date).ToList());
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _unitOfWorkService.PatientService.GetById(id);
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
