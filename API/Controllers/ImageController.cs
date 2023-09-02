using Business.Abstract;
using Core.Entities;
using Core.Utils.Exceptions;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ImageController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
         

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateImage create)
        {
                var response = await _unitOfWorkService.ImageService.AddAsync(create);
                return StatusCode((int)HttpStatusCode.Created, response);
        } 

    

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
                var response = await _unitOfWorkService.ImageService.DeleteAsync(id);
                return StatusCode((int)HttpStatusCode.NoContent, response);
        }
    }
}
