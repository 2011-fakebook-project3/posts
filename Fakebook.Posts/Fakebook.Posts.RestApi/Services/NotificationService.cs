using Fakebook.Posts.RestApi.DTOs;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService(HttpClient client) => throw new System.NotImplementedException();

        public async Task<HttpResponseMessage> SendNotificationAsync(string endpoint, NotificationDTO notification) => throw new System.NotImplementedException();
    }
}