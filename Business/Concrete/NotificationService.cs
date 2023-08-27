using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Abstract;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess;
using DataAccess.Abstract;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper )
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
        }

        public async  Task<Notification> AddAsync(CreateNotification entity)
        {
            try
            {
                var user = await _unitOfWorkRepository.UserRepository.GetByIdAsync(entity.UserId) ?? throw new NotFoundException($"User not found with id = {entity.UserId}");

                var newNotification = _mapper.Map<Notification>(entity);
                await _unitOfWorkRepository.NotificationRepository.AddAsync(newNotification);
                await _unitOfWorkRepository.SaveChangesAsync();



                return newNotification;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async  Task<Notification> DeleteAsync(int id)
        {
            try
            {
                var exist = await _unitOfWorkRepository.NotificationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Notification not found with id = {id}");
                _unitOfWorkRepository.NotificationRepository.Delete(id);
                await _unitOfWorkRepository.SaveChangesAsync();

                return exist;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public  IQueryable<NotificationResponse> GetAll()
        {
            var allNotifications = _unitOfWorkRepository.NotificationRepository.GetAll()
                .ProjectTo<NotificationResponse>(_mapper.ConfigurationProvider);

            return allNotifications;
        }

        public async  Task<NotificationResponse> GetByIdAsync(int id)
        {

            var notification = await _unitOfWorkRepository.NotificationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Notification not found with id = {id}");
            var response = _mapper.Map<NotificationResponse>(notification);

            return response;
        }

 

        public async  Task<Notification> UpdateAsync(int id, UpdateNotification entity)
        {
            var exist = await _unitOfWorkRepository.NotificationRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Notification not found with id = {id}");
            entity.Id = id;
            var notification = _mapper.Map(entity, exist);
            _unitOfWorkRepository.NotificationRepository.Update(notification);
            await _unitOfWorkRepository.SaveChangesAsync();

            return notification;
        }
        public async Task<IQueryable<NotificationResponse>> GetByUserIdAsync(int userId)
        {
            var user = await _unitOfWorkRepository.UserRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException($"User not found with id = {userId}");

            DateTime currentDate = DateTime.Now.Date;

            var allNotifications = _unitOfWorkRepository.NotificationRepository
                .GetAll(not => not.UserId.Equals(userId) &&
                        not.NotificationDate.Year == currentDate.Year &&
                        not.NotificationDate.Month == currentDate.Month &&
                        not.NotificationDate.Day == currentDate.Day)
                .ProjectTo<NotificationResponse>(_mapper.ConfigurationProvider);


            return allNotifications;
        }
    }

}
