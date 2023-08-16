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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ImageService : BaseService<Image, UpdateImage, CreateImage,ImageResponse>, IImageService
    {
        public ImageService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals) : base(unitOfWorkRepository, mapper, logService, globals)
        {
        }

        public override async Task<Image> AddAsync(CreateImage entity)
        {
            try
            {
                var newImage = _mapper.Map<Image>(entity);
                await _unitOfWorkRepository.ImageRepository.AddAsync(newImage);
                await SaveChangesAsync();
                _logService.Log("New Image added succesfully");
                return newImage;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public async override Task<Image> DeleteAsync(int id)
        {
            try
            {
                var exist = IsExist(id);
                _unitOfWorkRepository.ImageRepository.Delete(id);
                await SaveChangesAsync();
                _logService.Log($"Image Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override IQueryable<ImageResponse> GetAll()
        {
            try
            {
                var allImagesResponse = _unitOfWorkRepository.ImageRepository.GetAll()
                    .ProjectTo<ImageResponse>(_mapper.ConfigurationProvider);
                //var allImagesResponse = _mapper.Map<IQueryable<ImageResponse>>(result);
                _logService.Log($"All Images Selected");
             
                return allImagesResponse;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override ImageResponse GetById(int id)
        {
            try
            {
                _logService.Log($"Select Image byId = {id}");
                _= IsExist(id);
                var image = _unitOfWorkRepository.ImageRepository.GetById(id);
                var response  = _mapper.Map<ImageResponse>(image);
                return response;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override Image IsExist(int id)
        {
            var image = _unitOfWorkRepository.ImageRepository.GetById(id);
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
                var exist = IsExist(id);
                entity.Id = id;
                var image = _mapper.Map(entity, exist);
                _unitOfWorkRepository.ImageRepository.Update(image);
                await SaveChangesAsync();
                _logService.Log($"Image updated with id = {id}");
                return image;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }

        }
    }
}
