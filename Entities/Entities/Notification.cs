using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notification:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime NotificationDate { get; set; }

    }
}
