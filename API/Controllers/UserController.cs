using Azure;
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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public UserController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _unitOfWorkService.UserService.GetAll().ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUser createUser)
        {
            var response = await _unitOfWorkService.UserService.AddAsync(createUser);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser login)
        {
            var auth = await _unitOfWorkService.UserService.Login(login);
            return Ok(auth);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUser updateUser)
        {
            var user = await _unitOfWorkService.UserService.UpdateAsync(id, updateUser);
            return StatusCode((int)HttpStatusCode.OK, user);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto tokenDto)
        {
            return Ok(await _unitOfWorkService.UserService.RefreshAccessTokenAsync(tokenDto));
        }
    }
}
