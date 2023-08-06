using Azure;
using Business.Abstract;
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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public UserController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var response =   _unitOfWorkService.UserService.GetAll().ToList();
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUser createUser)
        {
            try
            {
                var response = await _unitOfWorkService.UserService.AddAsync(createUser);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUser login)
        {
            try
            {
                var auth = await _unitOfWorkService.UserService.Login(login);
                return Ok(auth);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUser updateUser)
        {
            try
            {
                var user = await _unitOfWorkService.UserService.UpdateAsync(id, updateUser);
                return StatusCode((int)HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto tokenDto)
        {
            try
            { 
                return Ok(await _unitOfWorkService.UserService.RefreshAccessTokenAsync(tokenDto));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
