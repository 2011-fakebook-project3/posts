using Fakebook.Posts.RestApi.DTOs;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Fakebook.Posts.RestApi.Services
{
    public class NotificationService : INotificationService
    {
        private HttpClient _client;

        public NotificationService(HttpClient client) => _client = client;

        public async Task<HttpResponseMessage> SendNotificationAsync(string endpoint, NotificationDTO notification)
        {
            string jsonContent = JsonSerializer.Serialize(notification);
            string fullUri = $"http://fakebook-notifications-api:27017/notifications/{endpoint}";
            var response = await _client.PostAsync(fullUri, new StringContent(jsonContent));
            return response;
        }
    }
}