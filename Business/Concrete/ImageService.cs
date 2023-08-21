using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Abstract;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess.Abstract;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ImageService : BaseService<Image, UpdateImage, CreateImage,ImageResponse>, IImageService
    {
        public ImageService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals, IHttpContextAccessor httpContextAccessor) : base(unitOfWorkRepository, mapper, logService, globals, httpContextAccessor)
        {
        }

        public override async Task<Image> AddAsync(CreateImage entity)
        {
            try
            {
                var newImage = _mapper.Map<Image>(entity);
                await _unitOfWorkRepository.ImageRepository.AddAsync(newImage);
                await SaveChangesAsync();
                await _logService.InfoAsync("New Image added succesfully");
                return newImage;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :38 && ImageService.cs");
                throw;
            }
        }

        public async override Task<Image> DeleteAsync(int id)
        {
            try
            {
                var exist = await IsExistAsync(id);
                _unitOfWorkRepository.ImageRepository.Delete(id);
                await SaveChangesAsync();
             await   _logService.InfoAsync($"Image Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :55 && ImageService.cs");
                throw;
            }
        }

        public async override Task<IQueryable<ImageResponse>> GetAll()
        {
            try
            {
                var allImagesResponse = _unitOfWorkRepository.ImageRepository.GetAll()
                    .ProjectTo<ImageResponse>(_mapper.ConfigurationProvider);
                //var allImagesResponse = _mapper.Map<IQueryable<ImageResponse>>(result);
                 await _logService.InfoAsync($"All Images Selected");
             
                return allImagesResponse;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :73 && ImageService.cs");
                throw;
            }
        }

        public async override Task<ImageResponse> GetById(int id)
        {
            try
            {
               await _logService.InfoAsync($"Select Image byId = {id}");
                _= await IsExistAsync(id);
                var image = _unitOfWorkRepository.ImageRepository.GetByIdAsync(id);
                var response  = _mapper.Map<ImageResponse>(image);
                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :90 && ImageService.cs");
                throw;
            }
        }

        public override async Task<Image> IsExistAsync(int id)
        {
            var image = await _unitOfWorkRepository.ImageRepository.GetByIdAsync(id);
            return image ?? throw new NotFoundException($"Image not found with id = {id}");
        }

        public override async Task SaveChangesAsync()
        {
             
                await _unitOfWorkRepository.ImageRepository.SaveChangesAsync();
             
        }

        public async override Task<Image> UpdateAsync(int id, UpdateImage entity)
        {
            try
            {
                var exist = await IsExistAsync(id);
                entity.Id = id;
                var image = _mapper.Map(entity, exist);
                _unitOfWorkRepository.ImageRepository.Update(image);
                await SaveChangesAsync();
                await _logService.InfoAsync($"Image updated with id = {id}");
                return image;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :122 && ImageService.cs");
                throw;
            }

        }
    }
}
