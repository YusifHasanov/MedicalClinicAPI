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
    public class ImageService : IImageService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
        }

        public async Task<Image> AddAsync(CreateImage entity)
        {
            var newImage = _mapper.Map<Image>(entity);
            await _unitOfWorkRepository.ImageRepository.AddAsync(newImage);
            await _unitOfWorkRepository.SaveChangesAsync();
            return newImage;
        }

        public async Task<Image> DeleteAsync(int id)
        {
            var exist = await _unitOfWorkRepository.ImageRepository.GetByIdAsync(id) ??
                        throw new NotFoundException($"Image not found with id = {id}");
            
            _unitOfWorkRepository.ImageRepository.Delete(id);
            await _unitOfWorkRepository.SaveChangesAsync();

            return exist;
        }
    }
}