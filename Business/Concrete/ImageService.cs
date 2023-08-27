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
    public class ImageService :  IImageService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper )
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper; 
        }

        public  async Task<Image> AddAsync(CreateImage entity)
        {
                var newImage = _mapper.Map<Image>(entity);
                await _unitOfWorkRepository.ImageRepository.AddAsync(newImage);
                await _unitOfWorkRepository.SaveChangesAsync();
                return newImage;
        }

        public async  Task<Image> DeleteAsync(int id)
        {

                var exist = await _unitOfWorkRepository.ImageRepository.GetByIdAsync(id)  ?? throw new NotFoundException($"Image not found with id = {id}");
                _unitOfWorkRepository.ImageRepository.Delete(id);
                await _unitOfWorkRepository.SaveChangesAsync();

                return exist;
            }

        public   IQueryable<ImageResponse> GetAll()
        {
                var allImagesResponse = _unitOfWorkRepository.ImageRepository.GetAll()
                    .ProjectTo<ImageResponse>(_mapper.ConfigurationProvider);
                //var allImagesResponse = _mapper.Map<IQueryable<ImageResponse>>(result);
                return allImagesResponse;
            
        }

        public async  Task<ImageResponse> GetByIdAsync(int id)
        {
                var image = await _unitOfWorkRepository.ImageRepository.GetByIdAsync(id)  ?? throw new NotFoundException($"Image not found with id = {id}"); 
                var response = _mapper.Map<ImageResponse>(image);
                return response;
        }

 
        public async  Task<Image> UpdateAsync(int id, UpdateImage entity)
        {
                var exist = await _unitOfWorkRepository.ImageRepository.GetByIdAsync(id)  ?? throw new NotFoundException($"Image not found with id = {id}");
                entity.Id = id;
                var image = _mapper.Map(entity, exist);
                _unitOfWorkRepository.ImageRepository.Update(image);
                await _unitOfWorkRepository.SaveChangesAsync();

                return image;
        }
    }
}
