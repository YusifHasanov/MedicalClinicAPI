using AutoMapper;
using Business.Abstract;
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
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        public DoctorController(IUnitOfWorkService unitOfWorkService,IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper)
        {
            _unitOfWorkService = unitOfWorkService;
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                
                return Ok(await (await _unitOfWorkService.DoctorService.GetAll()).ToListAsync());
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
                var response = await _unitOfWorkService.DoctorService.GetById(id);

                return Ok(response);
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
        public async Task<IActionResult> Add([FromBody] CreateDoctor create)
        {
            try
            {
                var response = await _unitOfWorkService.DoctorService.AddAsync(create);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctor update)
        {
            try
            {
                var response = await _unitOfWorkService.DoctorService.UpdateAsync(id, update);
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
                var response = await _unitOfWorkService.DoctorService.DeleteAsync(id);
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
