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
    public class NotificationService : BaseService<Notification, UpdateNotification, CreateNotification, NotificationResponse>, INotificationService
    {
        public NotificationService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals, IHttpContextAccessor httpContextAccessor) : base(unitOfWorkRepository, mapper, logService, globals, httpContextAccessor)
        {
        }

        public async override Task<Notification> AddAsync(CreateNotification entity)
        {
            try
            {
                var user = await _unitOfWorkRepository.UserRepository.GetByIdAsync(entity.UserId) ?? throw new NotFoundException($"User not found with id = {entity.UserId}");

                var newNotification = _mapper.Map<Notification>(entity);
                await _unitOfWorkRepository.NotificationRepository.AddAsync(newNotification);
                await SaveChangesAsync();

                await _logService.InfoAsync($"New Notification added succesfully for userId = {user.Id}");

                return newNotification;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "NotificationService.cs AddAsync");
                throw;
            }
        }

        public async override Task<Notification> DeleteAsync(int id)
        {
            try
            {
                var exist = await IsExistAsync(id);
                _unitOfWorkRepository.NotificationRepository.Delete(id);
                await SaveChangesAsync();
                await _logService.InfoAsync($"Notification Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "NotificationService.cs DeleteAsync");
                throw;
            }

        }

        public async override Task<IQueryable<NotificationResponse>> GetAll()
        {

            try
            {
                var allNotifications = _unitOfWorkRepository.NotificationRepository.GetAll()
                    .ProjectTo<NotificationResponse>(_mapper.ConfigurationProvider);

                await _logService.InfoAsync($"All Notifications Selected");

                return allNotifications;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "NotificationService.cs GetAll");
                throw;
            }
        }

        public async override Task<NotificationResponse> GetById(int id)
        {
            try
            {
                await _logService.InfoAsync($"Select Notification byId = {id}");
                _ = await IsExistAsync(id);
                var notification = await _unitOfWorkRepository.NotificationRepository.GetByIdAsync(id);
                var response = _mapper.Map<NotificationResponse>(notification);
                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "NotificationService.cs GetById");
                throw;
            }
        }



        public async override Task<Notification> IsExistAsync(int id)
        {
            var notification = await _unitOfWorkRepository.NotificationRepository.GetByIdAsync(id);
            return notification ?? throw new NotFoundException($"Notification not found with id = {id}");
        }

        public override async Task SaveChangesAsync()
        {
            await _unitOfWorkRepository.NotificationRepository.SaveChangesAsync();
        }

        public async override Task<Notification> UpdateAsync(int id, UpdateNotification entity)
        {
            try
            {
                var exist = await IsExistAsync(id);
                entity.Id = id;
                var notification = _mapper.Map(entity, exist);
                _unitOfWorkRepository.NotificationRepository.Update(notification);
                await SaveChangesAsync();
                await _logService.InfoAsync($"Notification updated with id = {id}");
                return notification;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "NotificationService.cs UpdateAsync");
                throw;
            }
        }
        public async Task<IQueryable<NotificationResponse>> GetByUserIdAsync(int userId)
        {
            try
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

                await _logService.InfoAsync($"All Notifications Selected");

                return allNotifications;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "NotificationService.cs GetByUserId");
                throw;
            }
        }


    }
}
