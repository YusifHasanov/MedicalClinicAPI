using Core.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notification:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime NotificationDate { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

    }
}
