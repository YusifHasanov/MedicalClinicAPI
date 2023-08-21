using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.Request.Create
{
    public class CreateNotification:BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime NotificationDate { get; set; }
        public int UserId { get; set; }
    }
}
