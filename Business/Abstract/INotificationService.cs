using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INotificationService 
    {
        public Task<IQueryable<NotificationResponse>> GetByUserIdAsync(int userId );

        public IQueryable<NotificationResponse> GetAll();
        public Task<NotificationResponse> GetByIdAsync(int id);
        public Task<Notification> DeleteAsync(int id);
        public Task<Notification> UpdateAsync(int id, UpdateNotification entity);
        public Task<Notification> AddAsync(CreateNotification entity);

    }
}
