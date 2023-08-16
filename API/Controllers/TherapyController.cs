using Business.Abstract;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllTherapies()
        {
            try
            {
                var response = _unitOfWorkService.TherapyService.GetAll().ToList();
                return Ok(response);
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
        public IActionResult GetAllByDateInterval([FromBody] DateIntervalRequest interval)
        {
            try
            {
                return Ok(_unitOfWorkService.TherapyService.GetTherapiesByDateInterval(interval).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byPatientId/{patientId}")]
        public IActionResult GetAllByPatientId(int patientId)
        {
            try
            {
                return Ok(_unitOfWorkService.TherapyService.GetTherapiesByPatientId(patientId).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
