using Fakebook.Posts.RestApi.DTOs;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Services
{
    public interface INotificationService
    {
        public Task<HttpResponseMessage> SendNotificationAsync(string endpoint, NotificationDTO notification);
    }
}