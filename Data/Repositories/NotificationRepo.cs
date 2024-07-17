using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Data.Repositories
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationRepo> _logger;
        private readonly string _notificationApiUrl = "https://alert-api.com/alerts";

        public NotificationRepo(HttpClient httpClient, ILogger<NotificationRepo> logger) 
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> SendNotificationAsync(Order order)
        {
            try
            {
                var sendNotification = new DeliveryNotification()
                {
                   OrderId = order.OrderId,
                   Message = $"Alert for delivered item: Order Id - {order.OrderId}",
                   DateTime = DateTime.UtcNow
                };

                var response = await _httpClient.PostAsJsonAsync(_notificationApiUrl, sendNotification);
                
                if (response.IsSuccessStatusCode)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error: Unable to send notification for Order Id - {order.OrderId}");
                throw new ApplicationException("Unable to send notification from NotificationRepo", ex);
            }
            
        }
    }
}
