using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.DTOs
{
    public class NotificationDTO
    {
        public string LoggedInUser { get; set; }

        public string TriggeredUser { get; set; }

        public int PostId { get; set; }
    }
}