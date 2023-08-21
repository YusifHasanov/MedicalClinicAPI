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
    public interface INotificationService:IService<Notification,UpdateNotification,CreateNotification,NotificationResponse>
    {
        public Task<IQueryable<NotificationResponse>> GetByUserId(int userId );    
    }
}
