﻿using Business.Abstract;
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

        [HttpPost("/test")]
        public async Task<IActionResult> AddImage()
        {
            try
            { 
                var imageBytes = System.IO.File.ReadAllBytes("D:\\Projects\\VisualStudioProjects\\ecommerce\\Server\\Entities\\house.png");
                CreateImage imgg = new()
                {
                    ImageData = Convert.ToBase64String(imageBytes), 
                    PatientId = 5
                };

               await _unitOfWorkService.ImageService.AddAsync(imgg);


                
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        { 
            try
            {
                return Ok(await (await _unitOfWorkService.ImageService.GetAll()).ToListAsync());
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
                var result = await _unitOfWorkService.ImageService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] CreateImage create)
        {
            try
            {
                var response = await _unitOfWorkService.ImageService.AddAsync(create);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateImage update)
        {
            try
            {
                var response = await _unitOfWorkService.ImageService.UpdateAsync(id, update);
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
                var response = await _unitOfWorkService.ImageService.DeleteAsync(id);
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
